using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{
    Rigidbody2D rb;
    float direcaoMovimento; // <-- Vamos guardar -1 (esquerda), 1 (direita) ou 0 (parado)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direcaoMovimento = 1f; // <-- Queremos ir para a direita
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            direcaoMovimento = -1f; // <-- Queremos ir para a esquerda
        }
        else
        {
            direcaoMovimento = 0f; // <-- Não estamos pressionando nada, ficar parado
        }
    }
    void FixedUpdate()
    {
        // Aplicamos a força baseada na direção que o Update() capturou
        rb.AddForce(Vector2.right * direcaoMovimento * 10f);
    }
}