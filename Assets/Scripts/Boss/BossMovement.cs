using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float speed;
    public float attackTriggerRange = 1f;
    public float attackCooldown = 1f;
    public float playerDetectRange = 5f;
    public float retreadDistance = 2f;
    public int facingDirection = -1;

    public Transform detectionPoint;
    public LayerMask playerLayer;
    public BossState bossState;

    private float attackCooldownTimer;
    private float savedSpeed;
    private bool isKiting = true;

    private BossAttackManager bossAttackManager;
    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;

    void Start(){
        int layerEnemy = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(layerEnemy, layerEnemy, true);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bossAttackManager = GetComponent<BossAttackManager>();
        savedSpeed = speed;
        ChangeState(BossState.Idle);
    }

    void Update()
    {
        if (bossState != BossState.Defeated)
        {
                CheckForPlayer();
                if (attackCooldownTimer > 0)
                {
                    attackCooldownTimer -= Time.deltaTime;
                }
                if (bossState == BossState.Chasing)
                {
                    Chase();
                }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);

        if (hits.Length > 0)
        {
            player = hits[0].transform;
            float playerDistance = Vector2.Distance(transform.position, player.transform.position);

            if (playerDistance <= attackTriggerRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                bossAttackManager.DoAttack();
            }
            else if (bossState == BossState.Idle)
            {
                ChangeState(BossState.Chasing);
            }
            else if(bossState != BossState.Chasing){
                rb.linearVelocity = Vector2.zero;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(BossState.Idle);
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

        if (attackCooldownTimer > 0 && distanceToPlayer < retreadDistance)
        {   
            direction =  direction = new Vector2(player.position.x - transform.position.x, 0).normalized;
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

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isKiting = false;
            speed = collision.gameObject.GetComponent<PlayerMovement>().moveSpeed;
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isKiting = true;
            speed = savedSpeed;
        }
    }

    public void SetCooldown(float newCooldown)
    {
        attackCooldown = newCooldown;
    }
    
    public void SetSafeDistance(float newDistance){
        retreadDistance = newDistance;
    }

    public void SetAtkTriggerRange(float newRange){
        attackTriggerRange = newRange;
    }

    public void SetSpeed(float newSpeed){
        speed = newSpeed;
    }

    
    public void ChangeState(BossState newState)
    {
        // Si ya estamos en el estado que se quiere activar, no hacemos nada.
        if (bossState == newState) return;

        // 1. Apaga la animación del estado anterior (y todas las demás por seguridad)
        anim.SetBool("isIdle", false);
        anim.SetBool("isChasing", false);
        anim.SetBool("isMeleeAttacking", false);
        anim.SetBool("isChargedMelee", false);
        anim.SetBool("isRangeAttacking", false);
        anim.SetBool("isChargedRange", false);
        //anim.SetBool("isKnockback", false);
        //anim.SetBool("isStunned", false);
        anim.SetBool("isDefeated", false);

        // 2. Actualiza al nuevo estado
        bossState = newState;

        // 3. Activa la animación correspondiente al nuevo estado
        switch (bossState)
        {
            case BossState.Idle:
                anim.SetBool("isIdle", true);
                break;
            case BossState.Chasing:
                anim.SetBool("isChasing", true);
                break;
            case BossState.MeleeAttacking:
                anim.SetBool("isMeleeAttacking", true);
                break;
            case BossState.ChargedMelee:
                anim.SetBool("isChargedMelee", true);
                break;
            case BossState.RangeAttacking:
                anim.SetBool("isRangeAttacking", true);
                break;
            case BossState.ChargedRange:
                anim.SetBool("isChargedRange", true);
                break;
            case BossState.Defeated:
                anim.SetBool("isDefeated", true);
                break;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        // Define el color del gizmo para que sea visible
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
        Gizmos.DrawWireSphere(detectionPoint.position, attackTriggerRange);

    } 
}

public enum BossState
{
    Idle,
    Chasing,
    MeleeAttacking,
    ChargedMelee,
    RangeAttacking,
    ChargedRange,
    Defeated
}

