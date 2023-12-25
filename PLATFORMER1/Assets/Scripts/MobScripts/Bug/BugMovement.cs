using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMovement : SpawnBugs
{
    private float timer;
    private Vector2 randomDirection;
    Vector3 parentPosition;
    void Start()
    {
        // Initialize the timer and set the initial random direction
        timer = Random.Range(changeDirectionIntervalMin, changeDirectionIntervalMax);
        parentPosition = transform.parent.position;
        SetRandomDirection();


    }

    void Update()
    {
        // Move the bug in the current direction
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);

        // Update the timer
        timer -= Time.deltaTime;

        // Check if it's time to change direction
        if (timer <= 0f)
        {
            SetRandomDirection();
            // Reset the timer with a new random interval
            timer = Random.Range(changeDirectionIntervalMin, changeDirectionIntervalMax);
        }

        CheckBoundaries();
    }

    void SetRandomDirection()
    {
        // Generate a random direction vector
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    void CheckBoundaries()
    {
        // Get the current position
        Vector2 currentPosition = transform.position;

        // Check X-axis boundaries
        if (currentPosition.x > parentPosition.x + moveRangeX || currentPosition.x < parentPosition.x - moveRangeX)
        {
            // Reverse the direction on the X-axis
            randomDirection.x *= -1f;
        }

        // Check Y-axis boundaries
        if (currentPosition.y > parentPosition.y + moveRangeY || currentPosition.y < parentPosition.y - moveRangeY)
        {
            // Reverse the direction on the Y-axis
            randomDirection.y *= -1f;
        }
        // Clamp the position within the defined range
        // Update the position
    }
}
