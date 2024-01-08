using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pathing : MonoBehaviour
{

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;

    //stun
    private bool isMovementEnabled = true;

    // Update is called once per frame
    private void Update()
    {
        if (isMovementEnabled)
        {


            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
        else
        {
            
        }
    }

    // Function to stop movement for a specified duration
    public void StopMovementForSeconds(float duration)
    {
        StartCoroutine(StopMovementCoroutine(duration));
    }

    private IEnumerator StopMovementCoroutine(float duration)
    {
        isMovementEnabled = false;
        yield return new WaitForSeconds(duration);
        isMovementEnabled = true;
    }
}
