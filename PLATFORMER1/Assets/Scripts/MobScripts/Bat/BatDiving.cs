using System;
using Unity.Mathematics;
using UnityEngine;

public class BatDiving : MonoBehaviour
{

    [SerializeField] private Transform target;
    public float radius = 6f;
    public float initialForce = 6f;
    public float flightDistance = 3f;
    public float diveCooldown = 3f;
    public float flySpeed = 4f;
    private Rigidbody2D rb;
    private bool isDiving;
    private float targetYPos;
    private float targetXPos;
    private float startPosition;
    private float leftPoint;
    private float rightPoint;
    private float startTime;
    private bool isTiming;
    private bool hasDived; // So we don't apply dive cooldown until dived for the first time
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.velocity = new Vector2(0f, 0f);
        startPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.GetCurrentState() == PlayerManager.PlayerState.companion)
        {
            target = GameObject.FindGameObjectWithTag("Companion").transform;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            if (target.transform.parent != null)
            {
                target = target.transform.parent.transform;
            }
        }

        if (Vector3.Distance(transform.position, target.transform.position) <= radius && !isDiving)
        {
            if (Time.time - startTime > diveCooldown || !hasDived)
            {
                Dive();
            }
        }

        //deecelerate to fly speed when going back up
        if (isDiving && rb.velocity.y > 0)
        {
            float newVelocity = Mathf.Lerp(rb.velocity.x, flySpeed, Time.deltaTime);
            rb.velocity = new Vector2(newVelocity, rb.velocity.y);
        }

        if (isDiving && transform.position.y > startPosition && rb.velocity.y > 0)
        {
            isDiving = false;
            rb.gravityScale = 0;
            rightPoint = flightDistance + transform.position.x;
            Debug.Log(rightPoint);
            leftPoint = transform.position.x - flightDistance;
            Debug.Log(leftPoint);
            startTime = Time.time;
            isTiming = true;
            rb.velocity = new Vector2(flySpeed, 0f);
        }

        if (isTiming)
        {
            Fly();
        }

        //Change sprite direction
        sr.flipX = rb.velocity.x > 0 ? false : true;
    }

    void Dive()
    {
        hasDived = true;
        isTiming = false;
        targetXPos = target.position.x;
        targetYPos = target.position.y;

        isDiving = true;
        rb.gravityScale = -1f;
        float gravity = -1 * Physics2D.gravity.y;
        float deltaY = targetYPos - transform.position.y;

        float deltaX = targetXPos - transform.position.x;
        float initialVelocityY = -1 * (Mathf.Sqrt(-2 * gravity * deltaY));
        // This value is always practically 0. Abs val it to avoid errors but this var does not change much in overall calcs
        float rootArg = Mathf.Abs((initialVelocityY * initialVelocityY) + (2 * gravity * deltaY));

        // Check if calculation will produce a negative root if so return.
        if (rootArg < 0f)
        {
            return;
        }
        else
        {
            // Calculate the initial velocity and launch angle based on apex coordinates
            float timeToApex = (-1 * initialVelocityY + Mathf.Sqrt(rootArg)) / gravity;

            float initialVelocityX = (deltaX - (0.5f * timeToApex * timeToApex)) / timeToApex;

            // Apply the calculated velocity to the Rigidbody
            Vector2 velocity = new Vector2(initialVelocityX, initialVelocityY);
            rb.velocity = velocity;
        }
    }

    void Fly()
    {
        // Toggle direction when at either point
        if (transform.position.x > rightPoint || transform.position.x < leftPoint)
        {
            // Switch direction
            rb.velocity = new Vector2(-rb.velocity.x, 0f);
            Vector2 newPosition = transform.position;
            newPosition.x = Mathf.Clamp(newPosition.x, leftPoint, rightPoint);
            transform.position = newPosition;
        }
        // Move the bat
        rb.velocity = new Vector2(rb.velocity.x, 0f);

    }
}
