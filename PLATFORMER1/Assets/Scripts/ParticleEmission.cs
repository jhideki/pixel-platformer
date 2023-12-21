using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmission : MonoBehaviour
{
    [SerializeField] private ParticleSystem spores;

    public float timeBetweenSpores;
    private Animator anim;
    private float timer;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //Release particles after allocated amount of time
        if (timer > timeBetweenSpores && !spores.isPlaying)
        {
            timer -= timeBetweenSpores;
            spores.Play();
            anim.SetBool("releaseSpores", true);
        }

        // Reset animation once particles finish playing
        if (!spores.isPlaying)
        {
            anim.SetBool("releaseSpores", false);
        }


    }

}
