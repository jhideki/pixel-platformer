
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;


public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform mc;
    [SerializeField] private Transform companion;
    private Transform player;
    [SerializeField] private float Camera_maxx;
    [SerializeField] private float Camera_minx;

    private float Camera_up;
    private float Camera_down;
    private float Cameray_pos;
    private float Camerax_pos;

    [SerializeField] private float smoothing = 1f;
    [SerializeField] private float smoothingx = 5f;
    [SerializeField] private float Camera_offx;
    [SerializeField] private float Camera_offy;

    [SerializeField] private GameObject lane;
    [SerializeField] int numLanes;
    [SerializeField] private float spacing;


    [SerializeField] private List<GameObject> lanes;

    void Start()
    {
        //Position of the first lane
        float offsety = lane.transform.position.y;
        float offsetx = lane.transform.position.x;
        float yVal;
        player = mc;

        // Cycle through all lanes and add them to lanes list
        for (int i = 0; i < numLanes; i++)
        {
            yVal = offsety + (i * spacing);
            lanes.Add(Object.Instantiate(lane, new Vector3(offsetx, yVal, 0), Quaternion.identity));
        }
    }

    private void Update()
    {
        // set camera to follow either mc or companion
        if (PlayerManager.instance.GetCurrentState() == PlayerManager.PlayerState.companion)
        {
            player = companion;
        }
        else
        {
            player = mc;
        }
        if (player.position.x >= Camera_minx && player.position.x <= Camera_maxx)
        {
            Camerax_pos = player.position.x;
        }

        checkLanes();
        //smooth in the y direction
        float smoothedPositionY = Mathf.Lerp(transform.position.y, Cameray_pos + Camera_offy, smoothing * Time.deltaTime);
        //smooth in the x direction
        float smoothedPositionX = Mathf.Lerp(transform.position.x, Camerax_pos + Camera_offx, smoothingx * Time.deltaTime);
        //smooth when dashing in the x direction
        transform.position = new Vector3(Camerax_pos + Camera_offx, smoothedPositionY, transform.position.z);
        /*
         * Dash logic
           if (movementScript.hasDashed)
           {
           Debug.Log("camera is smoothing");
           transform.position = new Vector3(smoothedPositionX, smoothedPositionY, transform.position.z);
           }
           else if (!movementScript.hasDashed && !movementScript.IsDashing)
           {
           transform.position = new Vector3(Camerax_pos + Camera_offx, smoothedPositionY, transform.position.z);
       }
       */

    }

    private void checkLanes()
    {
        if (lanes.Count > 1)
        {
            int i = 0;
            for (i = 0; i < lanes.Count - 1; ++i)
            {
                if ((player.position.y > lanes[i].GetComponent<Transform>().position.y) && (player.position.y <= lanes[i + 1].GetComponent<Transform>().position.y))
                {
                    Cameray_pos = lanes[i].GetComponent<Transform>().position.y;

                    break;
                }
            }

            if (i == lanes.Count - 1)
            {
                Cameray_pos = lanes[i - 1].GetComponent<Transform>().position.y;
                Debug.Log("lane pos: " + Cameray_pos);
            }

        }
        else
        {
            Cameray_pos = lanes[0].GetComponent<Transform>().position.y;

        }
    }



}
