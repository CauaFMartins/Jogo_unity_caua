using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Configurações de Ataque")]
    public Transform attackPoint; // Arraste o objeto "AttackPoint" aqui
    public float attackRange = 0.5f; // Tamanho da bolinha de ataque
    public LayerMask enemyLayers; // Selecione a layer "Enemy" no Inspector
    public float damageAmount = 10f;

    [Header("Inputs (Devem ser iguais ao PlayerMovement)")]
    public KeyCode fightModeKey = KeyCode.LeftShift;
    public KeyCode attackKey = KeyCode.Z;

    // Referência ao script de movimento para saber se está no chão
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Só ataca se estiver segurando Shift, apertar Z e estiver no chão (mesma lógica da animação)
        // Nota: Precisamos checar se o playerMovement existe para evitar erros
        bool isGrounded = Physics2D.OverlapCircle(playerMovement.groundCheck.position, playerMovement.groundCheckRadius, playerMovement.groundLayer);

        if (Input.GetKey(fightModeKey) && Input.GetKeyDown(attackKey) && isGrounded)
        {
            Attack();
        }
    }

    void Attack()
    {
        // 1. Detectar inimigos na área de alcance
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // 2. Aplicar dano neles
        foreach (Collider2D enemy in hitEnemies)
        {
            // Verifica se o objeto tem o script EnemyHealth
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
        }
    }

    // Desenha a bolinha vermelha no editor para facilitar o ajuste
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}