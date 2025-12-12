using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("Configurações de UI")]
    public TextMeshProUGUI textoMoedas;
    public GameObject painelVitoria; // Arraste o PainelVitoria para cá

    private int moedasColetadas = 0;
    private int totalDeMoedasNaFase = 19;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // CONTAGEM AUTOMÁTICA:
        // Procura todos os objetos que têm a Tag "Moeda" (ou o nome do seu script)
        // Certifique-se de que suas moedas tenham a Tag "Moeda" ou use o tipo do script:
        totalDeMoedasNaFase = GameObject.FindObjectsOfType<CoinCounter>().Length;
        
        Debug.Log("Total de moedas na fase: " + totalDeMoedasNaFase);
        
        AtualizarInterface();
        
        // Garante que o painel de vitória comece escondido
        if(painelVitoria != null) painelVitoria.SetActive(false);
    }

    public void GanharMoeda(int quantidade)
    {
        moedasColetadas += quantidade;
        AtualizarInterface();

        // VERIFICAÇÃO DE VITÓRIA
        if (moedasColetadas >= totalDeMoedasNaFase)
        {
            FinalizarJogo();
        }
    }

    void AtualizarInterface()
    {
        // Exemplo: "Moedas: 1 / 10"
        textoMoedas.text = $"Moedas: {moedasColetadas} / {totalDeMoedasNaFase}";
    }

    void FinalizarJogo()
    {
        Debug.Log("Todas as moedas coletadas! Fim de jogo.");
        
        // Ativa a tela de vitória
        if(painelVitoria != null)
        {
            painelVitoria.SetActive(true);
        }

        // Opcional: Pausar o jogo
        // Time.timeScale = 0; 
    }
}