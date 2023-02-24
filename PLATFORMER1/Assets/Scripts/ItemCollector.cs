using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{

    private int souls = 0;

    [SerializeField] private Text soulsText;

    // on collision with item
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Soul"))
        {
            Destroy(collision.gameObject);
            souls++;
            soulsText.text = "Souls: " + souls;
        }
    }
}
