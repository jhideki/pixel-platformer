using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMC : MonoBehaviour
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

    void Update()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);

        if (reachedTarget && movementScript.isIdle)
        {
            IdleMovement();
        }
        else
        {
            MoveTowardsTarget(targetPosition);
        }
    }

    void MoveTowardsTarget(Vector3 targetPosition)
    {
        smoothedPositionY = Mathf.Lerp(transform.position.y, targetPosition.y, smoothing * Time.deltaTime);
        smoothedPositionX = Mathf.Lerp(transform.position.x, targetPosition.x, smoothing * Time.deltaTime);
        transform.position = new Vector3(smoothedPositionX, smoothedPositionY, transform.position.z);

        if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f)
        {
            reachedTarget = true;
            targetIdlePosition = player.position.x + idleMovementDistance;
            StartCoroutine(Sleep());
        }
        else
        {
            reachedTarget = false;
        }
    }

    void IdleMovement()
    {
        if (Mathf.Abs(transform.position.x - targetIdlePosition) < 0.1f)
        {
            idleMovementDistance *= -1;
            targetIdlePosition = player.position.x + idleMovementDistance;
        }

        Vector3 targetVector = new Vector3(targetIdlePosition, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetVector, idleMoveSpeed * Time.deltaTime);
    }

    IEnumerator Sleep()
    {
        yield return new WaitForSeconds(waitTime);
    }
}
