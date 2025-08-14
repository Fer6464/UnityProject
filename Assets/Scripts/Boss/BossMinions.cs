using UnityEngine;

public class BossMinions : MonoBehaviour
{
    public float speed = 5f;
    public float playerDetectRange = 60f;
    public int facingDirection = 1;
    public Transform detectionPoint;
    public LayerMask playerLayer;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Chase();
    }



    void Chase()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, playerDetectRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            if (player.position.x > transform.position.x && facingDirection == -1 ||
                player.position.x < transform.position.x && facingDirection == 1)
            {
                Flip();
            }
            float directionX = Mathf.Sign(player.position.x - transform.position.x);
            rb.linearVelocity = new Vector2(directionX * speed, 0f);
        }
    }
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
  
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-1);
                col.gameObject.GetComponent<PlayerMovement>().Knockback(transform, 0f, 0.3f, 0.2f);
                Debug.Log("Bala dio al jugador! -1 de vida");
                Destroy(gameObject);
            }
            
        }
    
    
}
