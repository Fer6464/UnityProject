using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    void Start()
    {
        
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("GameOver");

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
