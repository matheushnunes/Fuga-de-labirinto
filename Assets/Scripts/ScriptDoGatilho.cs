using UnityEngine;

public class ScriptDoGatilho : MonoBehaviour
{
    [Header("Configuração da Plataforma")]
    public Transform plataformaParaAtivar; 
    public float alturaDeSubida = 5f;      
    public float velocidade = 2f;
    
    [Header("Configuração de Retorno")]
    [Tooltip("Tempo em segundos que a plataforma espera no topo antes de voltar")]
    public float tempoDeEspera = 3f; // <-- NOVO!

    // --- Variáveis de Controle ---
    private Vector3 posicaoInicial;
    private Vector3 posicaoAlvo;
    
    private bool estaSubindo = false;
    private bool estaVoltando = false;
    private float timerAtual = 0f;

    // Roda UMA VEZ no começo do jogo
    void Start()
    {
        // 1. Salva a posição inicial (onde ela começa)
        posicaoInicial = plataformaParaAtivar.position;
        
        // 2. Calcula a posição alvo (para onde ela vai)
        posicaoAlvo = new Vector3(
            plataformaParaAtivar.position.x,
            plataformaParaAtivar.position.y + alturaDeSubida,
            plataformaParaAtivar.position.z
        );
    }

    // Roda quando o jogador TOCA o gatilho
    void OnTriggerEnter2D(Collider2D outro)
    {
        // Se for o Jogador E a plataforma estiver parada (nem subindo, nem voltando)
        if (outro.gameObject.layer == LayerMask.NameToLayer("Jogador") && !estaSubindo && !estaVoltando)
        {
            // Inicia o movimento de subida
            estaSubindo = true;
        }
    }

    // Roda a CADA FRAME
    void Update()
    {
        // ----- 1. LÓGICA DE SUBIDA -----
        if (estaSubindo)
        {
            // Move a plataforma para a posicaoAlvo
            plataformaParaAtivar.position = Vector3.MoveTowards(
                plataformaParaAtivar.position,
                posicaoAlvo,
                velocidade * Time.deltaTime
            );

            // Checa se já chegou no alvo
            if (plataformaParaAtivar.position == posicaoAlvo)
            {
                estaSubindo = false; // Para de subir
                timerAtual = tempoDeEspera; // Inicia o timer
            }
        }
        
        // ----- 2. LÓGICA DO TIMER (Quando está no topo) -----
        if (timerAtual > 0)
        {
            timerAtual -= Time.deltaTime; // Diminui o timer
            
            // Se o timer acabou de acabar
            if (timerAtual <= 0)
            {
                estaVoltando = true; // Inicia o movimento de volta
            }
        }
        
        // ----- 3. LÓGICA DE DESCIDA (Volta) -----
        if (estaVoltando)
        {
            // Move a plataforma de volta para a posicaoInicial
            plataformaParaAtivar.position = Vector3.MoveTowards(
                plataformaParaAtivar.position,
                posicaoInicial,
                velocidade * Time.deltaTime
            );

            // Checa se já chegou na origem
            if (plataformaParaAtivar.position == posicaoInicial)
            {
                estaVoltando = false; // Para de descer (pronta para ser ativada de novo)
            }
        }
    }
}