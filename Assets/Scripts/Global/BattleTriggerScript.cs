using UnityEngine;

public class BattleTriggerScript : MonoBehaviour
{
    public Transform EnemyGeneratorManager;
    public AudioClip fight;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
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
            audioSource.PlayOneShot(fight, 0.5f);
            Debug.Log("La batalla ha empezado");
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("Trigger Destruido Exitosamente");
    }

}

