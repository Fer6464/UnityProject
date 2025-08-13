using UnityEngine;

public class BattleTriggerScript : MonoBehaviour
{
    public Transform EnemyGeneratorManager;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            foreach (Transform child in EnemyGeneratorManager)
            {
                if (child.CompareTag("EnemyGenerator"))
                {
                    child.gameObject.GetComponent<ObjectGenerator>().generateObjects();
                }
            }
            Debug.Log("La batalla ha empezado");
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("Trigger Destruido Exitosamente");
    }

}

