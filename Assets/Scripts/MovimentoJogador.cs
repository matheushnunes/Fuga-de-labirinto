using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{
    Rigidbody2D rb;
    float direcaoMovimento; // <-- Vamos guardar -1 (esquerda), 1 (direita) ou 0 (parado)
    bool querPular = false;

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

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)){
            querPular = true;
        }
    }
    void FixedUpdate()
    {
        // Aplicamos a força baseada na direção que o Update() capturou
        rb.AddForce(Vector2.right * direcaoMovimento * 10f);

        if (querPular == true)
        {
            // Aplicamos o pulo como um "Impulso" (um chute instantâneo)
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse); 
            querPular = false; // <-- Abaixa a bandeira para não pular de novo
        }
    }
}