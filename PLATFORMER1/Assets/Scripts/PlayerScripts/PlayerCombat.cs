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
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        data.runDeccelAmount *= 0.05f;
        anim.SetTrigger("attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit");
            enemy.GetComponent<Enemy>().takeDamage(100);
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
