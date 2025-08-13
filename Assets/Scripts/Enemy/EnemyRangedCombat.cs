using UnityEngine;
using System.Collections;

public class EnemyRangedCombat : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // Un objeto vacío que marca de dónde sale la bala.
    public EnemyMovement enemyMovement;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        EnemyBulletManager bulletScript = bulletInstance.GetComponent<EnemyBulletManager>();
        Vector2 direction = enemyMovement.facingDirection == 1 ? Vector2.right : Vector2.left;
        bulletScript.SetDirection(direction);
    }
}
