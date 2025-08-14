using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAttack : MonoBehaviour
{
    public float knockbackForce = 25;
    public float stunTime = 0.5f;
    public float knockBackTime = 0.15f;
    public int damage = 1;
    public float cooldown = 1f;
    public float weaponRange = 1;

    [Header("Interact Settings")]
    public float interactRange = 1.5f; // Rango para recoger objetos

    public Transform attackPoint;
    public LayerMask enemyLayer;
    public Animator anim;
    public AudioClip[] hitSounds;

    private float timer;
    private Controls controls;
    private InputAction attackAction;
    private AudioSource myHitSound;
    private PlayerMovement playerMovement;
    private InputAction interactAction;
    void Awake()
    {
        controls = new Controls();
        attackAction = controls.Player.Attack;
        myHitSound = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        interactAction = controls.Player.Interact;

    }
    private void OnEnable()
    {
        attackAction.Enable();
        attackAction.performed += DoAttack;
        interactAction.Enable(); 
        interactAction.performed += DoInteract;
    }
    private void OnDisable()
    {
        attackAction.performed -= DoAttack;
        attackAction.Disable();
        interactAction.performed -= DoInteract;
        interactAction.Disable();
    }
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

    }
    public void DoAttack(InputAction.CallbackContext context)
    {
        //Se puede escribir comparando el estado en una lista
        if (timer <= 0 && playerMovement.playerState != PlayerState.Knockback
        && playerMovement.playerState != PlayerState.Stunned
        && playerMovement.playerState != PlayerState.Defeated)
        {
            Debug.Log("¡Ataque! 💥");
            playerMovement.ChangeState(PlayerState.Attacking);
            timer = cooldown;
        }
    }
    
    private void DoInteract(InputAction.CallbackContext context)
    {
        // Buscamos todos los colliders cercanos en un círculo
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, interactRange);

        foreach (Collider2D obj in nearbyObjects)
        {
            // Si encontramos un objeto con el tag "Duff"...
            if (obj.CompareTag("Duff"))
            {
                Debug.Log("¡Duff recogida! 🍺");
                
                // Lo destruimos
                Destroy(obj.gameObject);

                // Llamamos al LevelManager para que cambie de nivel
                LevelManager.instance.GoToNextLevel();
                
                // Rompemos el bucle para no recoger más de un objeto a la vez
                break; 
            }
        }
    }
    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);
        if (enemies.Length > 0)
        {
            if (enemies[0].isTrigger)
            {
                return;
            }
            int randomIndex = Random.Range(0, hitSounds.Length);
            AudioClip actualSoundFX = hitSounds[randomIndex];
            myHitSound.PlayOneShot(actualSoundFX, 0.5f);
            foreach (Collider2D enemy in enemies)
            {
                if (enemy.isTrigger)
                    continue;

                EnemyHealth health = enemy.GetComponent<EnemyHealth>();
                if (health != null)
                    health.ChangeHealth(-damage);

                EnemyKnockback knock = enemy.GetComponent<EnemyKnockback>();
                if (knock != null)
                    knock.Knockback(transform, knockbackForce, knockBackTime, stunTime);
            }
        }
    }

    public void finishAttack()
    {
        playerMovement.ChangeState(PlayerState.Idle);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
        Gizmos.color = Color.blue; 
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}

