using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 60f;
    public float currentHealth;

    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage, Vector2 knockbackDirection, float knockbackForce)
    {
        currentHealth -= damage;

        // Aplica knockback
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Inimigo morreu!");
        gameObject.SetActive(false);
    }
}
