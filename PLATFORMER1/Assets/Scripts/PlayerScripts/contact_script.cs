using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contact_script : MonoBehaviour
{
    private bool isHeavyAttack = false;
    private bool isLightAttack = false;

    void Update()
    {
        // Check for key presses
        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isHeavyAttack = true;
            isLightAttack = false;
        }
        */
        if (Input.GetKeyDown(KeyCode.E))
        {
            isHeavyAttack = false;
            isLightAttack = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Deal damage to the enemy or perform other actions
            Enemy enemyComponent = other.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                int damage = 0;

                if (isHeavyAttack)
                {
                    damage = 50; // Heavy attack with 200 damage
                    Debug.Log("Heavy attack - Dealt " + damage + " damage to enemy");
                }
                else if (isLightAttack)
                {
                    damage = 50; // Light attack with 50 damage
                    Debug.Log("Light attack - Dealt " + damage + " damage to enemy");
                }
                else
                {
                   /* damage = 50; // Default damage for other cases
                    Debug.Log("Dealt " + damage + " damage to enemy");
                   */
                }

                enemyComponent.takeDamage(damage);
            }

            // Reset attack flags
            isHeavyAttack = false;
            isLightAttack = false;
        }
    }
}
//enemyComponent.takeDamage(100);