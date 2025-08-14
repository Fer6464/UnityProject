using UnityEngine;
using TMPro;


public class PlayerPointsManager : MonoBehaviour
{
    private int enemiesToDefeat;
    private int enemiesDefeated;
    public TMP_Text Defeated;
    public TMP_Text Left;

    void Start()
    {
        if (Defeated != null)
            Defeated.text = "Defeated: " + enemiesDefeated;

        if (Left != null)
            Left.text = "Left: " + (enemiesToDefeat - enemiesDefeated);
    }
    public void addEnemiesToDefeat()
    {
        enemiesToDefeat++;
        if (Defeated != null)
            Defeated.text = "Defeated: " + enemiesDefeated;

        if (Left != null)
            Left.text = "Left: " + (enemiesToDefeat - enemiesDefeated);
        Debug.Log("Tope nuevo: " + enemiesToDefeat);
    }

    public void addEnemyDefeated()
    {
        enemiesDefeated++;
        if (Defeated != null)
            Defeated.text = "Defeated: " + enemiesDefeated;
        if (Left != null)
            Left.text = "Left: " + (enemiesToDefeat - enemiesDefeated);
        Debug.Log("Enemigos derrotados: " + enemiesDefeated);
    }

    public bool hasDefeatedAllEnemies()
    {
        return enemiesDefeated == enemiesToDefeat;
    }
}
