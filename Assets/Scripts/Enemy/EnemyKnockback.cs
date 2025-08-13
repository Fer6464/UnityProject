using System.Collections;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    private Coroutine stunCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void Knockback(Transform playerTransform, float knockbackForce, float knockBackTime, float stunTime)
    {
        if (stunCoroutine != null)
        {
            StopCoroutine(stunCoroutine);
        }
        if (enemyMovement.enemyState != EnemyState.Defeated)
        {
            enemyMovement.ChangeState(EnemyState.Knockback);
            stunCoroutine = StartCoroutine(StunTimer(knockBackTime, stunTime));
            Vector2 direction = new Vector2(transform.position.x - playerTransform.position.x, 0).normalized;
            rb.linearVelocity = direction * knockbackForce;
            Debug.Log("Knockback aplied");
        }
    }

    IEnumerator StunTimer(float knockBackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockBackTime);
        enemyMovement.ChangeState(EnemyState.Stunned);
        yield return new WaitForSeconds(stunTime);
        enemyMovement.ChangeState(EnemyState.Idle);
    }
}
