using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int maxHealth = 100;
    private int curHealth;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        curHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {

        curHealth -= damage;
        if (curHealth <= 0)
        {
            die();
        }
    }

    void die()
    {
        anim.SetBool("isDead", true);
        Destroy(gameObject);
    }
}
