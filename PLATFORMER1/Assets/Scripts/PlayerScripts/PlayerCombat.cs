using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    private Animator anim;
    public PlayerRunData data;
    public Rigidbody2D RB;
    private PlayerMovement2 playerScript;

    private float oldMovementDeacceleration;
    [SerializeField] private Transform attackPoint;

    [SerializeField] private float attackRange = 0.5f;

    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int lightAttackDamage = 50;
    [SerializeField] private int heavyAttackDamage = 100;
    [SerializeField] private GameObject hitbox;
    [SerializeField] private float chainedAttackTime; // time period in which player must attack again to chain animations
    [SerializeField] private float secondAttackOffsetTime; // time before second attack animation is enabled (prevents chain attack from occuring immediatly after the first attack)
    [SerializeField] private float attackCooldownTime; // time before player can attack again 
    [SerializeField] private float impulseStrength; // time before player can attack again 
    [SerializeField] private float impulseFriction; // time before player can attack again 

    private bool isAttacking = false;
    private float attackCooldownlight = 1.0f;
    private float attackCooldownheavy = 1.5f;
    private float nextAttackTime = 0f;
    private string animType; // animation trigger (set to singleAttack to not chain attacks)
    private float startTime;
    private bool isTiming;
    private int numAttacks;
    private Vector2 direction;

    void Start()
    {
        anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        playerScript = GetComponent<PlayerMovement2>();
        animType = "attack1";
        numAttacks = 0;
    }

    void Update()
    {
        if(isTiming)
        {
            data.canMove = false;
            ApplyFriction();
        }else
        {
            data.canMove = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && numAttacks < 2)
        {
           if(!playerScript.IsFacingRight)
           {
               direction = new Vector2(-1f,0f);
           }else
           {
               direction = new Vector2(1f,0f);
           }

            //start defualt attack (this runs the first time player presses 'e')
            if(!isTiming)
            {
                startTime = Time.time;
                isTiming = true;
                anim.SetTrigger("attack"); 
                anim.SetBool("isChainAttack",false); 
                numAttacks++;
                attack();
            }
            // player pressed 'e' again so set chain attack animation
            if(Time.time - startTime < chainedAttackTime && Time.time - startTime > secondAttackOffsetTime) 
            {
                anim.SetBool("isChainAttack",true); 
                numAttacks++;
                startTime = Time.time; // reset the timer for attack cooldown
                isTiming = false;
                attack();
            }

            oldMovementDeacceleration = data.runAcceleration;
        }
        // check if enough time has elapsed before player can attack again
        if(Time.time - startTime >= attackCooldownTime)
        {
            numAttacks = 0;
            isTiming = false;
        }
    }

    void attack()
    {
        isAttacking = true;
        RB.AddForce(direction * impulseStrength, ForceMode2D.Impulse);
        nextAttackTime = Time.time + attackCooldownlight;
        data.runAcceleration*= 0.05f;
        data.runAcceleration = oldMovementDeacceleration;
        isAttacking = false;
    }

    void ApplyFriction()
    {
        Vector2 currentVelocity = RB.velocity;
        currentVelocity.x *= (1f - impulseFriction * Time.deltaTime);
        RB.velocity = currentVelocity;
    }

    // for debugging
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
