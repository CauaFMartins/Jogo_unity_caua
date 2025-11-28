using UnityEngine;
using System.Collections.Generic;

public class EnemyWaypointMovement : MonoBehaviour
{
    // --- Waypoints and Movement Settings (Do Código 1 e 2) ---
    [Header("Waypoints")]
    public List<Transform> waypoints;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float waypointReachedDistance = 0.1f;
    public bool loop = true;

    [Header("Visual")]
    public Transform visual; // Elemento para aplicar o 'flip' visual.

    // --- Combat Settings (Do Código 2) ---
    [Header("Combat Settings")]
    public float damage = 10f;
    public float attackCooldown = 1f;
    public float knockbackForce = 15f;

    // --- Private Variables ---
    private Rigidbody2D rb;
    private int currentWaypointIndex = 0;
    private Vector2 movementDirection;
    private float lastAttackTime; // Para controlar o cooldown do ataque.

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned to the enemy!");
            enabled = false;
            return;
        }

        SetTargetWaypoint(currentWaypointIndex);
        FlipVisual(); // Define o visual inicial
    }

    void FixedUpdate()
    {
        MoveTowardsWaypoint();
        CheckIfWaypointReached();
    }

    /// <summary>
    /// Define a direção para o próximo waypoint.
    /// </summary>
    void SetTargetWaypoint(int index)
    {
        if (waypoints.Count == 0) return;

        currentWaypointIndex = index;
        Vector2 targetPosition = waypoints[currentWaypointIndex].position;
        movementDirection = (targetPosition - (Vector2)transform.position).normalized;
    }

    /// <summary>
    /// Move o inimigo usando Rigidbody2D.
    /// </summary>
    void MoveTowardsWaypoint()
    {
        if (waypoints.Count == 0) return;

        Vector2 targetPosition = waypoints[currentWaypointIndex].position;
        // Recalcula a direção continuamente (mantém-se atualizado)
        movementDirection = (targetPosition - (Vector2)transform.position).normalized;

        // Aplica a velocidade APENAS no eixo X (mantém a gravidade/movimento do eixo Y do RB)
        rb.linearVelocity = new Vector2(movementDirection.x * moveSpeed, rb.linearVelocity.y);

        // Chamada de FlipVisual movida para GoToNextWaypoint() para o movimento,
        // mas pode ser chamado aqui também se quiser o flip contínuo.
        // Vamos manter o flip dentro de MoveTowardsWaypoint para reverter o visual
        // se a direção mudar *durante* a aproximação (ex: se o alvo estiver ligeiramente acima/abaixo)
        // FlipVisual(); 
    }

    /// <summary>
    /// Vira o visual do inimigo com base na direção X do movimento.
    /// (Adota a lógica mais simples de FlipVisual do Código 2, mas sem o fator de escala `* 0.32f`)
    /// </summary>
    void FlipVisual()
    {
        if (visual == null) return;

        // Se movendo para a direita (X positivo)
        if (movementDirection.x > 0.01f) 
        {
            // Vira o visual para a direita (escala X positiva)
            visual.localScale = new Vector3(Mathf.Abs(visual.localScale.x), visual.localScale.y, visual.localScale.z);
        }
        // Se movendo para a esquerda (X negativo)
        else if (movementDirection.x < -0.01f)
        {
            // Vira o visual para a esquerda (escala X negativa)
            visual.localScale = new Vector3(-Mathf.Abs(visual.localScale.x), visual.localScale.y, visual.localScale.z);
        }
    }

    /// <summary>
    /// Verifica se a distância para o waypoint atual é suficiente para mudar para o próximo.
    /// </summary>
    void CheckIfWaypointReached()
    {
        if (waypoints.Count == 0) return;

        float distanceToWaypoint = Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position);

        if (distanceToWaypoint <= waypointReachedDistance)
        {
            GoToNextWaypoint(); // Corrigido para o nome GoToNextWaypoint (o original do primeiro script)
        }
    }

    /// <summary>
    /// Atualiza o índice do waypoint e lida com o looping.
    /// </summary>
    void GoToNextWaypoint()
    {
        currentWaypointIndex++;

        if (currentWaypointIndex >= waypoints.Count)
        {
            if (loop)
            {
                currentWaypointIndex = 0;
            }
            else
            {
                enabled = false;
                rb.linearVelocity = Vector2.zero;
                return;
            }
        }

        SetTargetWaypoint(currentWaypointIndex);
        FlipVisual(); // Chama o flip para virar o inimigo na nova direção!
    }

    // --- Lógica de Combate (Do Código 2) ---

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TryAttackPlayer(collision.gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TryAttackPlayer(collision.gameObject);
        }
    }

    void TryAttackPlayer(GameObject player)
    {
        // Verifica o cooldown de ataque
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Calcula a direção do knockback (do inimigo para o jogador)
                Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;
                
                // Aplica dano e knockback
                playerHealth.TakeDamage(damage, knockbackDirection, knockbackForce);
                
                // Reinicia o timer do cooldown
                lastAttackTime = Time.time;
            }
        }
    }
    
    // Função Jump adicionada (do Código 2)
    public void Jump(float jumpForce)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
}