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

    void Start()
    {
        mag_lim = 0;
        ammoScript = Health.GetComponent<BulletCounter>();
        companion = GetComponent<CompanionMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (mag_lim < 3)
            {
                shoot();
            }
            mag_lim++;
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
}
