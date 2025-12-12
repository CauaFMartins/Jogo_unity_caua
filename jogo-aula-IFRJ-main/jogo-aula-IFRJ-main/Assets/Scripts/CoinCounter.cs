using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Verifica se quem encostou na moeda tem a TAG "Player"
        if (other.CompareTag("Player"))
        {
            // 2. Avisa o Manager para somar 1 moeda
            if (CoinManager.instance != null)
            {
                CoinManager.instance.GanharMoeda(1);
            }

            // 3. Destrói o objeto da moeda
            Destroy(gameObject);
        }
    }    
}