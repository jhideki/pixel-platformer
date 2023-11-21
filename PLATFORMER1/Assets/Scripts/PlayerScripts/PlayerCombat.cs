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

    void Start()
    {
        anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
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

    IEnumerator lightAttack()
    {
        data.runDeccelAmount *= 0.05f;
        anim.SetTrigger("lightAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {

            enemy.GetComponent<Enemy>().takeDamage(lightAttackDamage);
        }
        yield return new WaitForSeconds(0.04f);

        data.runDeccelAmount = oldMovementDeacceleration;

    }

    IEnumerator heavyAttack()
    {
        data.runDeccelAmount *= 0.05f;
        anim.SetTrigger("heavyAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDamage(heavyAttackDamage);
        }
        yield return new WaitForSeconds(0.04f);

        data.runDeccelAmount = oldMovementDeacceleration;
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
