using UnityEngine;

public class PlayerPointsManager : MonoBehaviour
{
    private int enemiesToDefeat;
    private int enemiesDefeated;

    public void addEnemiesToDefeat()
    {
        enemiesToDefeat++;
        Debug.Log("Tope nuevo: " + enemiesToDefeat);
    }

    public void addEnemyDefeated()
    {
        enemiesDefeated++;
        Debug.Log("Enemigos derrotados: " + enemiesDefeated);
    }

    public bool hasDefeatedAllEnemies()
    {
        return enemiesDefeated == enemiesToDefeat;
    }
}
