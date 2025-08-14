using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int healAmount = 1;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerHealth>().ChangeHealth(healAmount);
            Debug.Log("Curacion realizada: " + healAmount);
            Destroy(gameObject);
        }
    }
}
