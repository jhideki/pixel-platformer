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

    private bool isAttacking = false;
    private float attackCooldownlight = 1.0f;
    private float attackCooldownheavy = 1.5f;
    private float nextAttackTime = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isAttacking && Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                oldMovementDeacceleration = data.runDeccelAmount;
                lightAttack();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                oldMovementDeacceleration = data.runDeccelAmount;
                StartCoroutine(heavyAttack());
            }
        }
    }

   
    void lightAttack()

    {
        isAttacking = true;
        nextAttackTime = Time.time + attackCooldownlight;
        data.runDeccelAmount *= 0.05f;
        anim.SetTrigger("lightAttack"); 

        data.runDeccelAmount = oldMovementDeacceleration;
        isAttacking = false;
    }

    IEnumerator heavyAttack()
    {
        isAttacking = true;
        nextAttackTime = Time.time + attackCooldownheavy;
        data.runDeccelAmount *= 0.05f;
        anim.SetTrigger("heavyAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.layer != LayerMask.NameToLayer("Ground"))
            {
                enemy.GetComponent<Enemy>().takeDamage(heavyAttackDamage);
            }
        }
        yield return new WaitForSeconds(0.04f);

        data.runDeccelAmount = oldMovementDeacceleration;
        isAttacking = false;
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
