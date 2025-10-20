using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{
    Rigidbody2D rb;
    float direcaoMovimento; // <-- Vamos guardar -1 (esquerda), 1 (direita) ou 0 (parado)
    bool querPular = false;
    bool estaNoChao = false;
    private bool estaViradoParaDireita = true;
    public GameObject prefabDoTiro; // <-- O molde do tiro
    public Transform pontoDeDisparo; // <-- Onde o tiro vai nascer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            direcaoMovimento = 1f; // <-- Queremos ir para a direita
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            direcaoMovimento = -1f; // <-- Queremos ir para a esquerda
        }
        else
        {
            direcaoMovimento = 0f; // <-- Não estamos pressionando nada, ficar parado
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && estaNoChao == true) {
            querPular = true;
        }

        // Checa se o botão esquerdo do mouse foi pressionado
        if (Input.GetMouseButtonDown(0)) 
        {
            // 1. Cria uma cópia do prefab
            GameObject tiroInstanciado = Instantiate(prefabDoTiro, pontoDeDisparo.position, pontoDeDisparo.rotation);

            // 2. Pega o script que está no tiro que acabamos de criar
            ComportamentoTiro scriptDoTiro = tiroInstanciado.GetComponent<ComportamentoTiro>();

            // 3. Se o script existir, chama o novo método para definir a direção
            if (scriptDoTiro != null)
            {
                // Se estaViradoParaDireita for true, passa 1, senão, passa -1
                float direcaoDoTiro = estaViradoParaDireita ? 1f : -1f;

                scriptDoTiro.DefinirDirecao(direcaoDoTiro);
            }
        }
    }
    void FixedUpdate()
    {
        // --- NOSSO NOVO CÓDIGO DE VIRAR ---
        // Se estivermos indo para a direita (movimento > 0) E estivermos olhando para a esquerda...
        if (direcaoMovimento > 0 && !estaViradoParaDireita)
        {
            Virar();
        }
        // Se estivermos indo para a esquerda (movimento < 0) E estivermos olhando para a direita...
        else if (direcaoMovimento < 0 && estaViradoParaDireita)
        {
            Virar();
        }
        // --- FIM DO NOVO CÓDIGO ---

        // Aplicamos a força baseada na direção que o Update() capturou
        rb.AddForce(Vector2.right * direcaoMovimento * 10f);

        if (querPular == true)
        {
            // Aplicamos o pulo como um "Impulso" (um chute instantâneo)
            rb.AddForce(Vector2.up * 6f, ForceMode2D.Impulse); 
            querPular = false; // <-- Abaixa a bandeira para não pular de novo
        }
    }

    // Este método roda quando O JOGADOR ENCOSTA em algo
    void OnCollisionEnter2D(Collision2D colisao)
    {
        // Se a coisa que encostamos tiver a tag "Chao"
        if (colisao.gameObject.CompareTag("Chao"))
        {
            estaNoChao = true; // Estamos no chão!
        }
    }

    // Este método roda quando O JOGADOR PARA DE ENCOSTAR em algo
    void OnCollisionExit2D(Collision2D colisao)
    {
        // Se a coisa que paramos de encostar era o "Chao"
        if (colisao.gameObject.CompareTag("Chao"))
        {
            estaNoChao = false; // Não estamos mais no chão!
        }
    }

    // Este método roda quando o jogador "atravessa" um colisor "Is Trigger"
    void OnTriggerEnter2D(Collider2D outro)
    {
        // Se a coisa que atravessamos tiver a tag "ItemColetavel"...
        if (outro.gameObject.CompareTag("ItemColetavel"))
        {
            // ...destrói o item coletado.
            Destroy(outro.gameObject);
        }
    }

    void Virar()
    {
        // Inverte a flag
        estaViradoParaDireita = !estaViradoParaDireita;

        // Pega a escala atual do jogador
        Vector3 escala = transform.localScale;

        // Inverte o eixo X (multiplica por -1)
        escala.x *= -1; 

        // Aplica a nova escala
        transform.localScale = escala;
    }
}