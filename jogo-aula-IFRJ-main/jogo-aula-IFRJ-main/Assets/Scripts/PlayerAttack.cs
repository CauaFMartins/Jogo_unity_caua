using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public Transform attackPoint;          // Posição da hitbox
    public float attackRange = 0.5f;       // Tamanho da hitbox
    public float damage = 20f;
    public float knockbackForce = 10f;
    public LayerMask enemyLayers;

    private Animator anim;
    private bool canAttack = true;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Pressiona Z para atacar
        if (Input.GetKeyDown(KeyCode.Z) && canAttack)
        {
            anim.SetTrigger("Punch");
        }
    }

    // Chamado pela animação via Animation Event
    public void PerformAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hits)
        {
            EnemyHealth eHealth = enemy.GetComponent<EnemyHealth>();
            if (eHealth != null)
            {
                // Direção do knockback
                Vector2 knockDir = (enemy.transform.position - transform.position).normalized;

                eHealth.TakeDamage(damage, knockDir, knockbackForce);
            }
        }
    }

    // Apenas para visualizar a hitbox no editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
