using UnityEngine;

public class StartBossFight : MonoBehaviour
{
    public GameObject[] doors;
    private BossMovement bossMovement;
    void Start()
    {
        GameObject boss = GameObject.FindWithTag("Boss");
        bossMovement = boss.GetComponent<BossMovement>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<DoorManager>().StartBossFight();

                Debug.Log("Empieza BossFight");
            }
            bossMovement.playerDetectRange = 50f;
            Destroy(gameObject);
        }
       
    }
}
