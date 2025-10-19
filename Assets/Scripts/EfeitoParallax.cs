using UnityEngine;

public class EfeitoParallax : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 ultimaPosicaoCamera;

    [SerializeField] private float efeitoParallax = 0.5f; // Quão lento o fundo se move (0.5 = 50% da velocidade)

    void Start()
    {
        // Encontra a câmera principal
        cameraTransform = Camera.main.transform;
        ultimaPosicaoCamera = cameraTransform.position;
    }

    void LateUpdate() // LateUpdate roda DEPOIS que a câmera se moveu
    {
        // Calcula o quanto a câmera se moveu desde o último frame
        Vector3 deltaMovimento = cameraTransform.position - ultimaPosicaoCamera;

        // Move este objeto (o fundo) na mesma direção, mas multiplicado pelo efeito
        transform.position += new Vector3(deltaMovimento.x * efeitoParallax, deltaMovimento.y * efeitoParallax, 0);

        // Salva a posição da câmera para o próximo frame
        ultimaPosicaoCamera = cameraTransform.position;
    }
}