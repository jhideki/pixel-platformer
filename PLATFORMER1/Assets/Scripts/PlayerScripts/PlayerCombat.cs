using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    private Animator anim;
    public PlayerRunData data;
    public Rigidbody2D RB;

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

    private bool isAttacking = false;
    private float attackCooldownlight = 1.0f;
    private float attackCooldownheavy = 1.5f;
    private float nextAttackTime = 0f;
    private string animType; // animation trigger (set to singleAttack to not chain attacks)
    private float startTime;
    private bool isTiming;
    private int numAttacks;

    void Start()
    {
        anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        animType = "attack1";
        numAttacks = 0;
    }

    void Update()
    {
      if (Input.GetKeyDown(KeyCode.E) && numAttacks < 2)
      {
        //start defualt attack (this runs the first time player presses 'e')
        if(!isTiming)
        {
          startTime = Time.time;
          isTiming = true;
          anim.SetTrigger("attack"); 
          anim.SetBool("isChainAttack",false); 
          numAttacks++;
        }
        // player pressed 'e' again so set chain attack animation
        if(Time.time - startTime < chainedAttackTime && Time.time - startTime > secondAttackOffsetTime) 
        {
          anim.SetBool("isChainAttack",true); 
          numAttacks++;
          startTime = Time.time; // reset the timer for attack cooldown
          isTiming = false;
        }

        oldMovementDeacceleration = data.runAcceleration;
        attack();
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
      nextAttackTime = Time.time + attackCooldownlight;
      data.runAcceleration*= 0.05f;
      data.runAcceleration = oldMovementDeacceleration;
      isAttacking = false;
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
