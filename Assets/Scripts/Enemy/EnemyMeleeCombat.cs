using UnityEngine;

public class EnemyMeleeCombat : MonoBehaviour
{
    public int damage = 1;
    public Transform meleeHitbox;
    public float weapongRange;
    public LayerMask playerLayer;
    public float knockbackForce = 25;
    public float knockBackTime = 0.15f;
    public float stunTime = 0.5f;

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(meleeHitbox.position, weapongRange, playerLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
            Debug.Log("Golpe dio al jugador! -1 de vida");
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        // Define el color del gizmo para que sea visible
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeHitbox.position, weapongRange);
    } 
    
}
