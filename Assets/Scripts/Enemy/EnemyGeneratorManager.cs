using UnityEngine;

public class EnemyGeneratorManager : MonoBehaviour
{


    public void runGenerators()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<ObjectGenerator>().generateObjects();
        }
    }
}
