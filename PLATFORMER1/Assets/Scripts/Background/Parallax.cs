using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The further back an object is placed relative to the camera the more it follows the player
public class Parallax : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform subject;

    GameObject[] backgrounds = new GameObject[3];

    Vector2 startPosition;
    float startZ;

    private Transform backgroundLocation;

    Vector2 travel;
    float distanceFromSubject;
    float clippingPlane;
    float paralaxFactor;
    float backgroundWidth;

    // Start is called before the first frame update
    void Start()
    {
        backgroundLocation = GetComponent<Transform>();
        startPosition = transform.position;
        startZ = transform.position.z;
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        travel = (Vector2)cam.transform.position - startPosition;
        distanceFromSubject = transform.position.z - subject.position.z;
        clippingPlane = (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
        transform.position = startPosition + travel;
        paralaxFactor = Mathf.Abs(distanceFromSubject) / clippingPlane;


        Vector2 newPos = startPosition + travel * paralaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);


    }




}
