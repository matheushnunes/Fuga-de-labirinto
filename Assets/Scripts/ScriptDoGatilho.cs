using UnityEngine;

public class ScriptDoGatilho : MonoBehaviour
{
    [Header("Configuração da Plataforma")]
    public Transform plataformaParaAtivar; 
    
    // SUBSTITUÍMOS 'alturaDeSubida' POR ISTO:
    [Tooltip("Um objeto vazio que marca para onde a plataforma deve ir")]
    public Transform pontoDeDestino; // <-- NOSSO NOVO "ALVO"

    public float velocidade = 2f;
    
    [Header("Configuração de Retorno")]
    public float tempoDeEspera = 3f;

    // --- Variáveis de Controle ---
    private Vector3 posicaoInicial;
    private Vector3 posicaoAlvo;
    
    private bool estaSubindo = false; // (Agora significa "Indo para o Destino")
    private bool estaVoltando = false;
    private float timerAtual = 0f;

    // Roda UMA VEZ no começo do jogo
    void Start()
    {
        // 1. Salva a posição inicial (onde ela começa)
        posicaoInicial = plataformaParaAtivar.position;
        
        // 2. Define a posição alvo (baseada no objeto que arrastamos)
        if (pontoDeDestino != null)
        {
            posicaoAlvo = pontoDeDestino.position;
        }
        else
        {
            // Se esquecermos de conectar o Ponto de Destino, ele avisa no Console
            Debug.LogError("O 'Ponto De Destino' não foi definido para o gatilho: " + this.gameObject.name, this.gameObject);
        }
    }

    // Roda quando o jogador TOCA o gatilho
    void OnTriggerEnter2D(Collider2D outro)
    {
        // Se for o Jogador E a plataforma estiver parada (na origem)
        if (outro.gameObject.layer == LayerMask.NameToLayer("Jogador") && !estaSubindo && !estaVoltando)
        {
            // Inicia o movimento de ida
            estaSubindo = true;
        }
    }

    // Roda a CADA FRAME
    void Update()
    {
        // ----- 1. LÓGICA DE IDA (Indo para o Destino) -----
        if (estaSubindo)
        {
            // Move a plataforma para a posicaoAlvo (seja ela qual for)
            plataformaParaAtivar.position = Vector3.MoveTowards(
                plataformaParaAtivar.position,
                posicaoAlvo, 
                velocidade * Time.deltaTime
            );

            // Checa se já chegou no alvo
            if (plataformaParaAtivar.position == posicaoAlvo)
            {
                estaSubindo = false; // Para de ir
                timerAtual = tempoDeEspera; // Inicia o timer
            }
        }
        
        // ----- 2. LÓGICA DO TIMER (Quando está no destino) -----
        if (timerAtual > 0)
        {
            timerAtual -= Time.deltaTime; 
            
            if (timerAtual <= 0)
            {
                estaVoltando = true; // Inicia o movimento de volta
            }
        }
        
        // ----- 3. LÓGICA DE VOLTA (Voltando para a Posição Inicial) -----
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