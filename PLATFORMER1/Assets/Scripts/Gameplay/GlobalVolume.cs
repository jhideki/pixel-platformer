using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GlobalVolume : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private HealthController healthController;
    [SerializeField] private Transform vignetteVol;
    private Volume vignetteVolume;

    [SerializeField] private float transitionSpeed = 1.4f;
    [SerializeField] private float vignetteWeight1 = 0.4f;
    [SerializeField] private float vignetteWeight2 = 0.8f;
    private float startTime;
    private bool isTiming;
    private float currentWeight;


    // Start is called before the first frame update
    void Start()
    {
        vignetteVolume = vignetteVol.GetComponent<Volume>();
        isTiming = false;
        currentWeight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthController.playerHealth == 2 && currentWeight < vignetteWeight1)
        {

            currentWeight += transitionSpeed = Time.deltaTime;
            vignetteVolume.weight = currentWeight;
        }
        else if (healthController.playerHealth == 1 && currentWeight < vignetteWeight2)
        {
            currentWeight += transitionSpeed = Time.deltaTime;
            vignetteVolume.weight = currentWeight;
        }
    }
}
