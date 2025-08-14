using UnityEngine;
using System.Collections;

public class EnemyRangedCombat : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // Un objeto vacío que marca de dónde sale la bala.
    public EnemyMovement enemyMovement;
    public int facingDirection = -1;
    public AudioClip shootSound;
    private AudioSource audioSource;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        EnemyBulletManager bulletScript = bulletInstance.GetComponent<EnemyBulletManager>();
        Vector2 direction = enemyMovement.facingDirection == 1 ? Vector2.right : Vector2.left;
        audioSource.PlayOneShot(shootSound, 0.3f);
        bulletScript.SetDirection(direction);
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    
}
