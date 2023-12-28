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
    private bool isAttackingEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isAttackingEnabled)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            //Debug.Log(distance);


            if (distance < blast_rad)
            {
                timer += Time.deltaTime;

                if (timer > shot_space)
                {
                    timer = 0;
                    StartCoroutine(Shoot());
                }

            }
        }

    }

    IEnumerator Shoot()
    {
        if (isAttackingEnabled)
        {
            if (IsFacingRight())
            {
                if (player.transform.position.x >= transform.position.x && player.transform.position.y >= transform.position.y)
                {
                    anim.SetBool("Attck", true);
                    yield return new WaitForSeconds(0.5f);
                    Instantiate(bullet, bulletpos.position, Quaternion.identity);
                    anim.SetBool("Attck", false);

                }
            }
            else
            {
                if (player.transform.position.x <= transform.position.x && player.transform.position.y >= transform.position.y)
                {
                    anim.SetBool("Attck", true);
                    yield return new WaitForSeconds(0.5f);
                    Instantiate(bullet, bulletpos.position, Quaternion.identity);
                    anim.SetBool("Attck", false);
                }
            }
        }

    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    public void StopAttackingForSeconds(float duration)
    {
        StartCoroutine(StopAttackingCoroutine(duration));
    }

    // Coroutine to stop attacking for a specified duration
    private IEnumerator StopAttackingCoroutine(float duration)
    {
        isAttackingEnabled = false;
        yield return new WaitForSeconds(duration);
        isAttackingEnabled = true;
    }
}
