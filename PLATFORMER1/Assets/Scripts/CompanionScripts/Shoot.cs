using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    private int mag_lim;

    // Reference to the BulletCounter script
    [SerializeField] private Transform Health;
    [SerializeField] private Transform Player;
    private BulletCounter ammoScript;
    private CompanionMovement companion;

    // Trajectory preview variables
    public LineRenderer trajectoryLine;
    //private Stun match_force;
    public float launchForce = 15f;
    public float timeToDisplayTrajectory = 2f;

    private Vector3[] trajectoryPoints;
    private bool isTrajectoryPreviewActive = false;

    void Start()
    {
        mag_lim = 0;
        ammoScript = Health.GetComponent<BulletCounter>();
        companion = GetComponent<CompanionMovement>();
        //match_force = GetComponent<Stun>();

        // Initialize trajectory preview settings
        trajectoryPoints = new Vector3[10];
        trajectoryLine.positionCount = trajectoryPoints.Length;

        trajectoryLine.startColor = Color.white;  // Set your desired color
        trajectoryLine.endColor = Color.white;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (mag_lim < 10)
            {
                StartTrajectoryPreview();
            }
        }

        if (isTrajectoryPreviewActive)
        {
            UpdateTrajectoryPreview();
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            if (isTrajectoryPreviewActive)
            {
                if (mag_lim < 10)
                {
                    shoot();
                    mag_lim++;
                    isTrajectoryPreviewActive = false;
                    trajectoryLine.positionCount = 0;
                }
            }
        }
    }

    void shoot()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Stun bulletScript = bulletObject.GetComponent<Stun>();

        if (bulletScript != null)
        {
            bool isPlayerFacingRight = Player.GetComponent<PlayerMovement2>().IsFacingRight;
            Vector2 shootDirection = companion.isFacingRight? transform.right : -transform.right;
            bulletScript.Shoot(shootDirection);
        }

        if (ammoScript != null)
        {
            ammoScript.ShootBullet();
        }
    }

    void StartTrajectoryPreview()
    {
        isTrajectoryPreviewActive = true;
    }

    void UpdateTrajectoryPreview()
    {
        Debug.Log("Updating trajectory preview...");
        Vector3 initialPosition = firePoint.position;
        Vector3 initialVelocity = companion.isFacingRight? firePoint.right * launchForce: -firePoint.right * launchForce;

        for (int i = 0; i < trajectoryPoints.Length; i++)
        {
            float time = i * timeToDisplayTrajectory / trajectoryPoints.Length;
            float gravity = Physics2D.gravity.y;

            float x = initialPosition.x + initialVelocity.x * time;
            float y = initialPosition.y + initialVelocity.y * time + 0.5f * gravity * time * time;

            trajectoryPoints[i] = new Vector3(x, y, 0);
        }

        trajectoryLine.positionCount = trajectoryPoints.Length;
        trajectoryLine.SetPositions(trajectoryPoints);

    }

}
