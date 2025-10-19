using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{
    Rigidbody2D rb; // <-- 1. Declaramos a variável aqui

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // <-- 2. Inicializamos a variável aqui
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector2.right * 10f); // <-- Adiciona uma força para a direita
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector2.left * 10f); // <-- Adiciona uma força para a esquerda
        }
    }
}