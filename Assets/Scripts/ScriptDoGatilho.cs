using UnityEngine;

public class ScriptDoGatilho : MonoBehaviour
{
    // Vamos arrastar nossa plataforma aqui no Inspector
    public Transform plataformaParaAtivar; 

    void OnTriggerEnter2D(Collider2D outro)
    {
        // Se quem entrou no gatilho foi o Jogador (usando a Layer que criamos)...
        if (outro.gameObject.layer == LayerMask.NameToLayer("Jogador"))
        {
            // ...gira a plataforma em 90 graus
            plataformaParaAtivar.Rotate(0, 0, 90f);

            // Desativa o próprio gatilho para não rodar de novo
            gameObject.SetActive(false); 
        }
    }
}