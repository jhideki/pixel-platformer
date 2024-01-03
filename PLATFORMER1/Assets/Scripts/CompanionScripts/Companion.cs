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

    //for stun logic
    public GameObject bulletPrefab;
    public Transform firePoint;
    private int mag_lim;

    //ammo ui
    [SerializeField] private Transform Health; // Reference to the BulletCounter script
    private BulletCounter ammoScript;

    void Start()
    {
        mag_lim = 0;
        movementScript = player.GetComponent<PlayerMovement2>();
        ammoScript = Health.GetComponent<BulletCounter>();
    }

    void Update()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);

        if (reachedTarget && movementScript.isIdle)
        {
            idleMovement();
        } else
        {
            smoothedPositionY = Mathf.Lerp(transform.position.y, targetPosition.y, smoothing * Time.deltaTime);
            smoothedPositionX = Mathf.Lerp(transform.position.x, targetPosition.x, smoothing * Time.deltaTime);
            transform.position = new Vector3(smoothedPositionX, smoothedPositionY, transform.position.z);

            if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f) {
                reachedTarget = true;
                targetIdlePosition = player.position.x + idleMovementDistance;
                sleep();
            } else {
                reachedTarget = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed. Calling Shoot!");

            if(mag_lim < 3)
            {
                Shoot();
            }
            mag_lim++;
        }
        bool isPlayerOnRight = player.position.x > transform.position.x;
        bool isCompanionFacingRight = transform.localScale.x > 0f;

        if (!movementScript.isIdle && ((isPlayerOnRight && !isCompanionFacingRight) || (!isPlayerOnRight && isCompanionFacingRight)))
        {
            Turn();
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

        Vector3 targetVector = new Vector3(targetIdlePosition,transform.position.y,transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetVector,idleMoveSpeed*Time.deltaTime);
    }

    IEnumerator sleep()
    {
        yield return new WaitForSeconds(waitTime);
    }

    private void Turn()
    {
        // Get the player's scale
        Vector3 playerScale = player.localScale;

        // Set the companion's scale to be the opposite of the player's scale
        transform.localScale = new Vector3(-Mathf.Sign(playerScale.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void Shoot()
    {
        // Instantiate the bullet
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get the BulletScript component and call the Shoot method to set the direction
        stun bulletScript = bulletObject.GetComponent<stun>();

        if (bulletScript != null)
        {
            // Check if the player is facing right
            bool isPlayerFacingRight = player.GetComponent<PlayerMovement2>().IsFacingRight;

            // Determine the shoot direction based on player's facing direction
            Vector2 shootDirection = isPlayerFacingRight ? transform.right : -transform.right;
            //Debug.Log("stun script loaded successfully!");
            bulletScript.Shoot(shootDirection);
        }
        else
        {
            //Debug.LogError("Error: stun script not found on bullet object!");
        }

        if (ammoScript != null)
        {
            ammoScript.ShootBullet();
        }
    }
}
