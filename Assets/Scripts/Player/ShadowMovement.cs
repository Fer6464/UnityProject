using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    public Transform targetPlayer;
    void Start()
    {
        
    }


    void Update()
    {
        transform.position = new Vector3(targetPlayer.position.x, targetPlayer.position.y-1f, -1);
    }
}
