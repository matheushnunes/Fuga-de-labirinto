using UnityEngine;

public class ComportamentoTiro : MonoBehaviour
{
    public float velocidade = 10f; 
    private Rigidbody2D rb;

    // O Start() agora só pega o Rigidbody, mas não define a velocidade
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // --- ESTE É O NOSSO NOVO MÉTODO ---
    // Ele será chamado pelo MovimentoJogador
    public void DefinirDirecao(float direcao) // direcao será 1 (direita) ou -1 (esquerda)
    {
        // Se formos para a esquerda, vira o sprite do tiro
        if (direcao < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // (Não precisamos de um "else" porque o prefab já olha para a direita por padrão)

        // Define a velocidade usando a direção recebida
        // (Esperamos um frame para garantir que o Start() já rodou)
        Invoke("AplicarVelocidade", 0.01f);
    }

    // Tivemos que criar este método separado por causa do Invoke
    void AplicarVelocidade()
    {
        // Pega a direção da escala que acabamos de definir
        float direcao = transform.localScale.x; 

        rb.linearVelocity = new Vector2(velocidade * direcao, 0);
    }

    // O código de colisão permanece igual
    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Obstaculo"))
        {
            Destroy(colisao.gameObject);
            Destroy(gameObject);
        }

        if (colisao.gameObject.CompareTag("Chao"))
        {
            Destroy(gameObject);
        }
    }
}