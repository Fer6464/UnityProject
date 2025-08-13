using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float attackRangeX = 1f;
    public float attackRangeY = 1f;
    public float attackCooldown = 2f;
    public float playerDetectRange = 5f;
    public float retreatDistance = 2f;
    public Transform detectionPoint;
    public LayerMask playerLayer;
    public int facingDirection = -1;

    public EnemyState enemyState;
    private float attackCooldownTimer;

    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;

    void Start()
    {
        int layerEnemy = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(layerEnemy, layerEnemy, true);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState != EnemyState.Defeated)
        {
            if (enemyState != EnemyState.Knockback && enemyState != EnemyState.Stunned)
            {
                CheckForPlayer();
                if (attackCooldownTimer > 0)
                {
                    attackCooldownTimer -= Time.deltaTime;
                }
                if (enemyState == EnemyState.Chasing)
                {
                    Chase();
                }
                else if (enemyState == EnemyState.Attacking)
                {
                    rb.linearVelocity = Vector2.zero;
                }
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void Defeated()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerPointsManager>().addEnemyDefeated();
        }
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);

        if (hits.Length > 0)
        {
            player = hits[0].transform;
            float distanceX = Mathf.Abs(transform.position.x - player.transform.position.x);
            float distanceY = Mathf.Abs(transform.position.y - player.transform.position.y);
            bool isInAttackRange = distanceX <= attackRangeX && distanceY <= attackRangeY;

            if (isInAttackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    void Chase()
    {

        if (player.position.x > transform.position.x && facingDirection == -1 ||
            player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Vector2 direction = (player.position - transform.position).normalized;

        if (attackCooldownTimer > 0 && distanceToPlayer < retreatDistance)
        {
            rb.linearVelocity = -direction * speed * 0.35f;
        }
        else
        {
            rb.linearVelocity = direction * speed;
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }


    private void OnDrawGizmosSelected()
    {
        // Define el color del gizmo para que sea visible
        Gizmos.color = Color.red;

        // Calcula el centro del rectángulo de ataque
        // Usamos transform.position como centro
        Vector2 center = transform.position;

        // Calcula el tamaño del rectángulo
        // attackRangeX * 2 y attackRangeY * 2 porque el rango va desde el centro en ambas direcciones
        Vector2 size = new Vector2(attackRangeX * 2, attackRangeY * 2);

        Gizmos.DrawWireCube(center, size);
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    } 

    public void ChangeState(EnemyState newState)
    {
        if (enemyState == newState) return;

        anim.SetBool("isIdle", false);
        anim.SetBool("isChasing", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isKnockback", false);
        anim.SetBool("isStunned", false);
        anim.SetBool("isDefeated", false);

        enemyState = newState;

        switch (enemyState)
        {
            case EnemyState.Idle:
                anim.SetBool("isIdle", true);
                break;
            case EnemyState.Chasing:
                anim.SetBool("isChasing", true);
                break;
            case EnemyState.Attacking:
                anim.SetBool("isAttacking", true);
                break;
            case EnemyState.Knockback:
                anim.SetBool("isKnockback", true);
                break;
            case EnemyState.Stunned:
                anim.SetBool("isStunned", true);
                break;
            case EnemyState.Defeated:
                anim.SetBool("isDefeated", true);
                break;
        }
    }

}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Knockback,
    Stunned,
    Defeated
}