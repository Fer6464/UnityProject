using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    public AudioClip ostOnStart;
    public AudioClip ostOnLoop;
    public ObjectGenerator leftGenerator;
    public ObjectGenerator rightGenerator;
    public GameObject prefab; 
    private OSTmanager ostManager;
    private bool alreadyChangedOst;
    private BossMovement bossMovement;


    void Start()
    {
        GameObject musicplayer = GameObject.FindWithTag("MusicPlayer");
        ostManager = musicplayer.GetComponent<OSTmanager>();
        bossMovement = GetComponent<BossMovement>();
    }

    public void changeOst()
    {
        if (!alreadyChangedOst) {
            ostManager.ChangeStartOst(ostOnStart, ostOnLoop);
            bossMovement.ChangeState(BossState.SecondPhase);
            leftGenerator.minionsGen();
            rightGenerator.minionsGen();
            alreadyChangedOst = true;
        }
    }
}
