using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private float duration;
    private PlayerMovement2 playerScript;
    private bool startTimer;
    private float startTime;
    private BoxCollider2D[] colliders;

    void Start()
    {
        playerScript = player.GetComponent<PlayerMovement2>(); 
        colliders = GetComponents<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerScript.CanJump())
        {
            startTimer = true;
        }

    }
    void Update()
    {
        if(startTimer)
        {
            startTime = Time.time;
            startTimer = false;
        }

        if(Time.time - startTime > duration)
        {
            GetComponent<Renderer>().enabled = false;
            foreach(var collider in colliders)
            {
                collider.enabled = false;
            }

        }

    }
}
