using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    public int damage = 1;

    public float knockbackForce = 0f;
    public float knockBackTime = 0.25f;
    public float stunTime = 0f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    void Start()
    {
        rb.linearVelocity = moveDirection * bulletSpeed;
        Destroy(gameObject, 5f); 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
            col.gameObject.GetComponent<PlayerMovement>().Knockback(transform, knockbackForce, knockBackTime, stunTime);
            Debug.Log("Bala dio al jugador! -1 de vida");
            Destroy(gameObject);
        }
        
    }


}
