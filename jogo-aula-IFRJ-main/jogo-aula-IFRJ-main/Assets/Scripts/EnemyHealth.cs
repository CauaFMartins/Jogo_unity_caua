using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Configurações de Vida")]
    public float maxHealth = 50f;
    private float currentHealth;

    // Opcional: Animação de dano/morte
    private Animator anim; 

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>(); // Tenta pegar o animator se houver
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " sofreu " + damage + " de dano.");

        // Se tiver animação de "Hurt", ative aqui
        if(anim != null) anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " Morreu!");
        
        // Desativa o colisor e a gravidade para ele não interagir mais
        Collider2D col = GetComponent<Collider2D>();
        if(col != null) col.enabled = false;
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb != null) rb.bodyType = RigidbodyType2D.Static;

        // Toca animação de morte se tiver
        if(anim != null)
        {
            anim.SetTrigger("Die");
            Destroy(gameObject, 1f); // Espera 1 segundo da animação para destruir
        }
        else
        {
            Destroy(gameObject); // Destroi imediatamente se não tiver animação
        }
    }
}