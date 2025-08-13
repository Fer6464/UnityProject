using UnityEngine;

public class DoorManager : MonoBehaviour
{

    private PlayerPointsManager playerPointsManager;
    private BoxCollider2D bc;
    private bool isBossAwaken;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        GameObject player = GameObject.FindWithTag("Player");
        playerPointsManager = player.GetComponent<PlayerPointsManager>();
    }

    void Update()
    {
        if (!isBossAwaken)
        {
            if (playerPointsManager.hasDefeatedAllEnemies())
            {
                bc.isTrigger = true;
                Debug.Log("Puerta para el jefe abierta");
            }
            else
            {
                bc.isTrigger = false;
                Debug.Log("Puerta para el jefe cerrada");
            }
        }
    }

    public void StartBossFight()
    {
        isBossAwaken = true;
        bc.isTrigger = false;
    }
}
