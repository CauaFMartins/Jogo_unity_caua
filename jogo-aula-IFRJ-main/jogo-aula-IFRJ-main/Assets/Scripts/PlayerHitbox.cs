using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [Header("Configuração de Dano")]
    public float damage = 20f; // Quanto de dano o soco causa

    // Essa função roda automaticamente quando o Trigger da Hitbox toca em algo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se tocou em um inimigo (usando o script de vida dele)
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            // Causa o dano
            enemy.TakeDamage(damage);
        }
    }
}