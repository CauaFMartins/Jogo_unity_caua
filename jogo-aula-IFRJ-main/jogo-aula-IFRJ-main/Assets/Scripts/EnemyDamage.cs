using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Configuração de Ataque")]
    public float damage = 10f;       // Dano que o inimigo causa ao encostar
    public float knockbackForce = 10f; // Força do empurrão

    // Usa colisão física (não trigger) para detectar o contato com o corpo do player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Busca o script PlayerHealth que você JÁ TEM no seu personagem
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Calcula a direção do empurrão (Inimigo -> Jogador)
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                
                // Aplica o dano e o knockback usando sua função existente
                playerHealth.TakeDamage(damage, direction, knockbackForce);
            }
        }
    }
}