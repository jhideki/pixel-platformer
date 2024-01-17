using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private float duration;
    [SerializeField] private float animOffset;
    private PlayerMovement2 playerScript;
    private bool startTimer;
    private float startTime;
    private BoxCollider2D[] colliders;
    private Animator anim;
    private bool isTiming;


    void Start()
    {
        playerScript = player.GetComponent<PlayerMovement2>();
        colliders = GetComponents<BoxCollider2D>();
        anim = GetComponent<Animator>();
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
        if (startTimer)
        {
            startTime = Time.time;
            startTimer = false;
            isTiming = true;
        }

        if (Time.time - startTime > duration && isTiming)
        {
            GetComponent<Renderer>().enabled = false;
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
        }
        if (Time.time - startTime > animOffset && isTiming)
        {
            anim.SetTrigger("Break");
        }

    }
}
