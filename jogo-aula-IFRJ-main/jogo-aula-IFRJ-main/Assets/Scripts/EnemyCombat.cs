using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float damage = 10f;
    public float attackCooldown = 1f;
    public float knockbackForce = 15f;

    private float lastAttackTime;

    void OnCollisionEnter2D(Collision2D collision)
    {
        TryAttack(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        TryAttack(collision);
    }

    void TryAttack(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerHealth pHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (pHealth != null)
            {
                Vector2 knockDir = (collision.transform.position - transform.position).normalized;

                pHealth.TakeDamage(damage, knockDir, knockbackForce);

                lastAttackTime = Time.time;
            }
        }
    }
}
