using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contact_script : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Deal damage to the enemy or perform other actions
            Enemy enemyComponent = other.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.takeDamage(100);
                Debug.Log("Dealt damage to enemy");
            }
        }
    }
}
