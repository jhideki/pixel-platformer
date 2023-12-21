using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private float smoothing = 1.0f;
    [SerializeField] private float idleMoveSpeed;
    [SerializeField] private float idleMovementDistance = 1.0f;
    private float smoothedPositionY;
    private float smoothedPositionX;
    private PlayerMovement2 movementScript;
    private float targetIdlePosition;
    private bool reachedTarget;
    private float waitTime = 0.25f;

    void Start()
    {
        movementScript = player.GetComponent<PlayerMovement2>();
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);

        if(reachedTarget && movementScript.isIdle)
        {
            idleMovement();
        }else
        {
            smoothedPositionY = Mathf.Lerp(transform.position.y, targetPosition.y, smoothing * Time.deltaTime);
            smoothedPositionX = Mathf.Lerp(transform.position.x, targetPosition.x, smoothing * Time.deltaTime);
            transform.position = new Vector3(smoothedPositionX, smoothedPositionY, transform.position.z);

            if(Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f){
              reachedTarget = true;
              targetIdlePosition = player.position.x + idleMovementDistance;
              sleep();
            }else{
              reachedTarget = false;
            }
        }
    }

    void idleMovement()
    {
        // Change direction when reaching the target position
        if (Mathf.Abs(transform.position.x - targetIdlePosition) < 0.1f)
        {
          idleMovementDistance *= -1;
          targetIdlePosition = player.position.x + idleMovementDistance;
        }

        float distance = player.position.x - transform.position.x;
        // Calculate the time it would take to cover that distance at the desired speed
        float timeToReachTarget = Mathf.Abs(distance) / idleMoveSpeed;
        // Calculate the interpolation factor based on time
        float t = Mathf.Clamp01(Time.deltaTime / timeToReachTarget); 
        // Use smoothedPositionX for horizontal movement
        smoothedPositionX = Mathf.Lerp(transform.position.x, targetIdlePosition,t);

        // Use the same y position as the player during idle
        smoothedPositionY = Mathf.Lerp(transform.position.y, player.position.y + yOffset, smoothing * Time.deltaTime);

        transform.position = new Vector3(smoothedPositionX, smoothedPositionY, transform.position.z);
    }

    IEnumerator sleep()
    {
        yield return new WaitForSeconds(waitTime);
    }
}
