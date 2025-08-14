using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public float timeToWait = 5.0f;
    public int currentHealth;
    public int maxHealth;
    private BossMovement bossMovement;
    private BoxCollider2D bc;
    private BossFightManager bossFightManager;

    void Start()
    {
        currentHealth = maxHealth;
        bossMovement = GetComponent<BossMovement>();
        bossFightManager = GetComponent<BossFightManager>();
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
            bossMovement.ChangeState(BossState.Defeated);
            Destroy(bc);
        }
        else if (currentHealth <= maxHealth / 2)
        {
            bossFightManager.changeOst(); 
        }
    }

    public void Die()
    {
        SceneManager.LoadScene("Victory");
        //StartCoroutine(LoadSceneAfterDelay(timeToWait));
    }
    
    IEnumerator LoadSceneAfterDelay(float delay)
    {
        // Pausa la ejecución por el tiempo especificado
        yield return new WaitForSeconds(delay);

        // Se ejecuta después del retraso
        SceneManager.LoadScene("Victory");
    }
}
