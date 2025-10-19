using UnityEngine;

public class ComportamentoTiro : MonoBehaviour
{
    public float velocidade = 10f; // Velocidade do tiro
    private Rigidbody2D rb;

    void Start()
    {
        // Pega o Rigidbody2D do próprio tiro
        rb = GetComponent<Rigidbody2D>();

        // Faz o tiro voar para a direita (transform.right)
        // Usamos .velocity para dar uma velocidade constante
        rb.linearVelocity = transform.right * velocidade; 
    }

    // Este método roda quando o tiro ENCOSTA em algo
    void OnCollisionEnter2D(Collision2D colisao)
    {
        // Se a coisa que o tiro bateu tiver a tag "Obstaculo"...
        if (colisao.gameObject.CompareTag("Obstaculo"))
        {
            // ...destrói o obstáculo
            Destroy(colisao.gameObject);

            // E SÓ ENTÃO destrói o próprio tiro
            Destroy(gameObject);
        }

        // Se o tiro bater no "Chao", ele também deve ser destruído
        if (colisao.gameObject.CompareTag("Chao"))
        {
            Destroy(gameObject);
        }

        // Se bater em qualquer outra coisa (como o Jogador), ele ignora!
    }
}