using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public float timeToWait = 5.0f;
    public int currentHealth;
    public int maxHealth;
    private PlayerPointsManager playerPointsManager;
    private EnemyMovement enemyMovement;
    private EnemyKnockback enemyKnockback;
    private BoxCollider2D bc;

    void Start()
    {
        currentHealth = maxHealth;
        enemyMovement = GetComponent<EnemyMovement>();
        playerPointsManager = GetComponent<PlayerPointsManager>();
        GameObject player = GameObject.FindWithTag("Player");
        playerPointsManager = player.GetComponent<PlayerPointsManager>();
        enemyKnockback = GetComponent<EnemyKnockback>();
        playerPointsManager = GetComponent<PlayerPointsManager>();
        bc = GetComponent<BoxCollider2D>();
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth < 0)
        {
            //Destroy(gameObject , 5);
            enemyMovement.ChangeState(EnemyState.Defeated);
            Destroy(bc);

            StartCoroutine(LoadSceneAfterDelay(timeToWait));
        }
    }

    public void Die()
    {
        enemyMovement.Defeated();
        Destroy(gameObject);
    }
    
    IEnumerator LoadSceneAfterDelay(float delay)
    {
        // Pausa la ejecución por el tiempo especificado
        yield return new WaitForSeconds(delay);

        // Se ejecuta después del retraso
        SceneManager.LoadScene("Victory");
    }
}
