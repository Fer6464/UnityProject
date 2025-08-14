using UnityEngine;

public class StartBossFight : MonoBehaviour
{
    public GameObject[] doors;
    public AudioClip ostOnStart;
    public AudioClip ostOnLoop;
    private OSTmanager ostManager;
    private BossMovement bossMovement;
    void Start()
    {
        GameObject boss = GameObject.FindWithTag("Boss");
        GameObject musicplayer = GameObject.FindWithTag("MusicPlayer");
        ostManager = musicplayer.GetComponent<OSTmanager>(); 
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
            ostManager.ChangeStartOst(ostOnStart, ostOnLoop);
            Destroy(gameObject);
        }
       
    }
}
