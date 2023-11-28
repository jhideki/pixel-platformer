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
                StartCoroutine(lightAttack());
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                oldMovementDeacceleration = data.runDeccelAmount;
                StartCoroutine(heavyAttack());
            }
        }
    }

    IEnumerator lightAttack()
    { 
        isAttacking = true;
        nextAttackTime = Time.time + attackCooldownlight;
        data.runDeccelAmount *= 0.05f;
        anim.SetTrigger("lightAttack");

        // Assuming you have a separate collider for the hitbox
        Collider2D hitboxCollider = GetComponentInChildren<Collider2D>();

        //enemy.gameObject.layer != LayerMask.NameToLayer("Ground")/saving for later if needed
        if (hitboxCollider != null)
        {
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.layerMask = enemyLayers;

            List<Collider2D> hitEnemiesList = new List<Collider2D>();
            Physics2D.OverlapCollider(hitboxCollider, contactFilter, hitEnemiesList);

            foreach (Collider2D enemy in hitEnemiesList)
            {
                Debug.Log("Hit enemy: " + enemy.gameObject.name + ", Layer: " + LayerMask.LayerToName(enemy.gameObject.layer));
                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    // Deal damage to the enemy
                    enemyComponent.takeDamage(lightAttackDamage);
                    Debug.Log("Dealt damage to enemy");
                }
                else
                {
                    Debug.LogError("Enemy object is missing the Enemy component: " + enemy.gameObject.name);
                }

            }
        }
        yield return new WaitForSeconds(0.04f);

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
