using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBugs : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int minSwarm;
    [SerializeField] private int maxSwarm;
    [SerializeField] private GameObject bug;
    [SerializeField] private float xRange;
    [SerializeField] private float yRange;
    public float moveSpeed = 2f; // Adjust the speed as needed
    public float changeDirectionIntervalMin = 2f; // Minimum time before changing direction
    public float changeDirectionIntervalMax = 5f; // Maximum time before changing direction
    protected float moveRangeX = 2;
    protected float moveRangeY = 2;
    
    void Start()
    {
       SpawnSwarm(); 
    }

    // Update is called once per frame
    void SpawnSwarm()
    {
       for(int i = minSwarm; i < maxSwarm; i++){
         Vector2 spawnPosition = transform.position;
         spawnPosition.x += Random.Range(-xRange,xRange);
         spawnPosition.y += Random.Range(-yRange,yRange);
         GameObject newBug = Instantiate(bug, spawnPosition, Quaternion.identity);
         newBug.transform.parent = transform;
       } 
    }
}
