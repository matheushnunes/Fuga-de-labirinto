using UnityEngine;

public class ScriptDoGatilho : MonoBehaviour
{
    [Header("Configuração da Plataforma")]
    // Arraste a plataforma (elevador) aqui no Inspector
    public Transform plataformaParaAtivar; 
    
    // O quanto ela vai subir (ex: 5 unidades)
    public float alturaDeSubida = 5f;      
    
    // A velocidade do elevador
    public float velocidade = 2f;        

    // --- Variáveis de Controle ---
    private Vector3 posicaoAlvo;          // Onde a plataforma deve chegar
    private bool estaAtivada = false;     // O "interruptor" do elevador

    // Este método roda quando o jogador ENTRA no gatilho
    void OnTriggerEnter2D(Collider2D outro)
    {
        // Linha de Debug 1: Nos diz QUEM tocou o gatilho
        Debug.Log("GATILHO TOCADO POR: " + outro.gameObject.name + " | NO LAYER: " + LayerMask.LayerToName(outro.gameObject.layer));

        // Checa se quem entrou foi o Jogador (usando o Layer)
        if (outro.gameObject.layer == LayerMask.NameToLayer("Jogador"))
        {
            // Linha de Debug 2: Confirma que o "if" passou
            Debug.Log("IF STATEMENT PASSOU! Ativando a plataforma.");

            // 1. Calcula a posição final (Alvo)
            posicaoAlvo = new Vector3(
                plataformaParaAtivar.position.x, // Mantém o X
                plataformaParaAtivar.position.y + alturaDeSubida, // Adiciona a altura no Y
                plataformaParaAtivar.position.z  // Mantém o Z
            );
            
            // 2. Liga o "interruptor"
            estaAtivada = true; 
            
            // 3. Desativa o próprio gatilho para não rodar de novo
            gameObject.SetActive(false); 
        }
    }

    // Este método roda a CADA FRAME
    void Update()
    {
        // Se o interruptor estiver ligado (estaAtivada == true)...
        if (estaAtivada && plataformaParaAtivar != null)
        {
            // ...move a plataforma suavemente em direção ao "posicaoAlvo"
            plataformaParaAtivar.position = Vector3.MoveTowards(
                plataformaParaAtivar.position, // Posição atual
                posicaoAlvo,                   // Posição alvo
                velocidade * Time.deltaTime    // Velocidade
            );
        }
    }
}