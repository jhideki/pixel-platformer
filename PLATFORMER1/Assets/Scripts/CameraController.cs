using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float Camera_maxx;
    [SerializeField] private float Camera_minx;
    [SerializeField] private float Camera_up;
    private float Cameray_pos;
    private float Camerax_pos;
    [SerializeField] private float Camera_offx;
    [SerializeField] private float Camera_offy;

    private void start()
    {
     
    }
    // Update is called once per frame
    private void Update()
    {
        
        Debug.Log(player.position.y);
        if (player.position.x >= Camera_minx && player.position.x <= Camera_maxx)
        {
            Camerax_pos = player.position.x;
        }

        if(player.position.y >= Camera_up - 1 && player.position.y <= Camera_up + 1)
        {
            Debug.Log("faggot");
            Cameray_pos = player.position.y;
        }

        transform.position = new Vector3(Camerax_pos + Camera_offx, Cameray_pos + Camera_offy, transform.position.z);

    }

}
