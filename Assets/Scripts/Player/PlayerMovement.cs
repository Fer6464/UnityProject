using UnityEngine;
using System;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    Rigidbody2D rb;
    bool isFacingRight = true;
    Animator anim;
    public PlayerAttack playerAttack;
    public PlayerState playerState;

    private Coroutine stunCoroutine;
    private Vector2 movement;
    void Start()
    {
        rb  = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        FlipSprite();

    }

    private void FixedUpdate()
    {

        float velX = rb.linearVelocity.x;
        float velY = rb.linearVelocity.y;
        //Se puede escribir comparando el estado en una lista
        if (playerState != PlayerState.Knockback
        && playerState != PlayerState.Stunned
        && playerState != PlayerState.Defeated)
        {
            anim.SetFloat("xVelocity", Math.Abs(velX));
            anim.SetFloat("yVelocity", Math.Abs(velY));
            movement.Set(InputManager.Movement.x, InputManager.Movement.y);
            rb.linearVelocity = movement * moveSpeed;
            if ((velX != 0 || velY != 0) &&  playerState != PlayerState.Attacking)
            {
                ChangeState(PlayerState.Walking);
            }
            else if (velX == 0 && velY == 0 &&  playerState != PlayerState.Attacking)
            {
                ChangeState(PlayerState.Idle);;
            }
        }
    }

    void FlipSprite()
    {
        if ((isFacingRight && movement.x < 0f) || (!isFacingRight && movement.x > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
    public void Knockback(Transform enemy, float knockbackForce, float knockBackTime, float stunTime)
    {
        if (stunCoroutine != null)
        {
            StopCoroutine(stunCoroutine);
        }
        if (playerState != PlayerState.Defeated)
        {
            ChangeState(PlayerState.Knockback);
            stunCoroutine = StartCoroutine(StunTimer(knockBackTime, stunTime));
            Vector2 direction = new Vector2(transform.position.x - enemy.position.x, 0).normalized;
            rb.linearVelocity = direction * knockbackForce;
            Debug.Log("Knockback aplied");
        }
    }
    IEnumerator StunTimer(float knockBackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockBackTime);
        ChangeState(PlayerState.Stunned);
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        ChangeState(PlayerState.Idle);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss") && playerState != PlayerState.Knockback)
        {
            rb.linearVelocity = Vector2.zero;   
        }
        else if (collision.gameObject.CompareTag("Enemy") && playerState != PlayerState.Knockback)
        {
            Rigidbody2D rbEnemigo = collision.rigidbody;
            if (rbEnemigo != null)
                rbEnemigo.linearVelocity = Vector2.zero;
         }
    }
    public void ChangeState(PlayerState newState)
    {
        // Si ya estamos en el estado que se quiere activar, no hacemos nada.
        if (playerState == newState) return;

        // 1. Apaga todas las animaciones por seguridad
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isRangeAttacking", false);
        anim.SetBool("isKnockBack", false);
        anim.SetBool("isStunned", false);
        anim.SetBool("isInvulnerable", false);
        anim.SetBool("isDefeated", false);

        // 2. Actualiza al nuevo estado
        playerState = newState;

        // 3. Activa la animación correspondiente al nuevo estado
        switch (playerState)
        {
            case PlayerState.Idle:
                anim.SetBool("isIdle", true);
                break;
            case PlayerState.Walking:
                anim.SetBool("isWalking", true);
                break;
            case PlayerState.Attacking:
                anim.SetBool("isAttacking", true);
                break;
            case PlayerState.RangeAttacking:
                anim.SetBool("isRangeAttacking", true);
                break;
            case PlayerState.Knockback:
                anim.SetBool("isKnockBack", true);
                break;
            case PlayerState.Stunned:
                anim.SetBool("isStunned", true);
                break;
            case PlayerState.Invulnerable:
                anim.SetBool("isInvulnerable", true);
                break;
            case PlayerState.Defeated:
                anim.SetBool("isDefeated", true);
                break;
        }
    }
}

public enum PlayerState
{
    Idle,
    Walking,
    Attacking,
    RangeAttacking,
    Knockback,
    Stunned,
    Invulnerable,
    Defeated
}