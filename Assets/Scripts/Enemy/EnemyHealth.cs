using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    private PlayerPointsManager playerPointsManager;
    private EnemyMovement enemyMovement;
    private BoxCollider2D bc;
    private AudioSource audioSource;
    void Start()
    {
        currentHealth = maxHealth;
        enemyMovement = GetComponent<EnemyMovement>();
        playerPointsManager = GetComponent<PlayerPointsManager>();
        GameObject player = GameObject.FindWithTag("Player");
        playerPointsManager = player.GetComponent<PlayerPointsManager>();
        playerPointsManager = GetComponent<PlayerPointsManager>();
        audioSource = GetComponent<AudioSource>();
        bc = GetComponent<BoxCollider2D>();
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            //Destroy(gameObject , 5);
            enemyMovement.ChangeState(EnemyState.Defeated);
            Destroy(bc);
        }
    }

    public void DeadSound()
    {
        audioSource.Play();
    }
    
    public void Die()
    {
        enemyMovement.Defeated();
        Destroy(gameObject);
    }
}
