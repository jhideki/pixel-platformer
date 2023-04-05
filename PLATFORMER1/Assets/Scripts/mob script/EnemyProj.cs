using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProj : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletpos;
    [SerializeField] private float shot_space;
    [SerializeField] private float blast_rad;

    
    private Animator anim;
    private float timer;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);

        if(distance < blast_rad)
        {
            timer += Time.deltaTime;

            if (timer > shot_space)
            {
                timer = 0;               
                shoot();
            }

            
        }

        if(timer > 1.2)
        {
            anim.SetBool("Attck", false);
            Debug.Log("fag");
        }

    }

    void shoot()
    {
        if(IsFacingRight()){
            if (player.transform.position.x >= transform.position.x && player.transform.position.y >= transform.position.y){
                Instantiate(bullet, bulletpos.position, Quaternion.identity);
                anim.SetBool("Attck", true);
            }
        }

        else
        {
            if (player.transform.position.x <= transform.position.x && player.transform.position.y >= transform.position.y)
            {
                Instantiate(bullet, bulletpos.position, Quaternion.identity);
                anim.SetBool("Attck", true);
            }
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
}
