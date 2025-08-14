using UnityEngine;
using System.Collections.Generic;

public class BossAttackManager : MonoBehaviour
{
    public int damage = 1;
    public float weapongRange = 2f;
    public int attackMultiplier = 2;
    public float knockbackForce = 25;
    public float knockBackTime = 0.15f;
    public float stunTime = 0.5f;

    public Transform meleeHitbox;
    public Transform rangeShooter;
    public Transform playerTransform;
    public LayerMask playerLayer;
    public GameObject bulletPrefab;
    public AudioClip shootSound;
    public AudioClip doPunch;
    public AudioClip secondPhase;

    private AudioSource audioSource;
    private List<BossAttacks> attacks = new List<BossAttacks>();
    private BossAttacks savedAttack;
    BossMovement bossMovement;


    void Start()
    {

        bossMovement = GetComponent<BossMovement>();
        attacks.Add(new BossAttacks() { name = "Melee", action = MeleeAttack, probability = 0.3f, attackTriggerRange = 2.5f, bossState = BossState.MeleeAttacking });
        attacks.Add(new BossAttacks() { name = "ChargedMelee", action = ChargedMeleeAttack, probability = 0.3f, attackTriggerRange = 2.5f, bossState = BossState.ChargedMelee });
        attacks.Add(new BossAttacks() { name = "Ranged", action = RangedAttack, probability = 0.4f, attackTriggerRange = 6f, bossState = BossState.RangeAttacking });
        audioSource = GetComponent<AudioSource>();
        RollAttack();
    }

    public void DoAttack()
    {
        bossMovement.ChangeState(savedAttack.bossState);
        Debug.Log("Se realizara ataque");
    }

    public void RollAttack()
    {
        float roll = Random.value;
        float cumulative = 0f;

        foreach (var atk in attacks)
        {
            cumulative += atk.probability;
            if (roll <= cumulative)
            {
                savedAttack = atk;
                if (atk.name == "Ranged" || atk.name == "ChargedMelee")
                {
                    bossMovement.SetSafeDistance(atk.attackTriggerRange + 2f);
                }
                else
                {
                    bossMovement.SetSafeDistance(0f);
                }
                bossMovement.SetAtkTriggerRange(atk.attackTriggerRange);
                Debug.Log("Roll Exitoso");
                return;
            }
        }
    }

    private void MeleeAttack()
    {
        audioSource.PlayOneShot(doPunch, 0.5f);
        Collider2D[] hits = Physics2D.OverlapCircleAll(meleeHitbox.position, weapongRange, playerLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
            hits[0].GetComponent<PlayerMovement>().Knockback(transform, knockbackForce, knockBackTime, stunTime);
            Debug.Log("Golpe del jefe dio al jugador! -1 de vida");
        }
    }

    private void ChargedMeleeAttack()
    {
        audioSource.PlayOneShot(doPunch, 0.5f);
        Collider2D[] hits = Physics2D.OverlapCircleAll(meleeHitbox.position, weapongRange, playerLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage * attackMultiplier);
            hits[0].GetComponent<PlayerMovement>().Knockback(transform, knockbackForce * 2, knockBackTime, stunTime * 1.5f);
            Debug.Log("Golpe cargado del jefe dio al jugador! -" + (damage * attackMultiplier) + " de vida");
        }
    }

    private void RangedAttack()
    {
        audioSource.PlayOneShot(shootSound, 0.5f);
        GameObject bulletInstance = Instantiate(bulletPrefab, rangeShooter.position, Quaternion.identity);
        EnemyBulletManager bulletScript = bulletInstance.GetComponent<EnemyBulletManager>();
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        bulletScript.SetDirection(direction);
    }
    private void OnDrawGizmosSelected()
    {
        // Define el color del gizmo para que sea visible
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(meleeHitbox.position, weapongRange);

    }
    public void secondPhaseSFX()
    {
        audioSource.PlayOneShot(secondPhase, 0.5f);
    }
}

public class BossAttacks
{
    public string name;
    public System.Action action;
    public float probability;
    public float attackTriggerRange;
    public BossState bossState;
}