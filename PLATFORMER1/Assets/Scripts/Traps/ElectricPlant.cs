using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPlant : MonoBehaviour
{
    private enum MovementState {idle,start,maintain,end};
    private Animator anim;
    private BoxCollider2D collider;
    [SerializeField] GameObject lightOff;
    [SerializeField] GameObject lightOn;
    [SerializeField] private float timeIdle;
    [SerializeField] private float timeMaintain;
    [SerializeField] private float startTimeOffset;
    private bool isTimingIdle;
    private bool isTimingMaintain;
    private float startTimeIdle;
    private float startTimeMaintain;

    // Start is called before the first frame update
    void Start()
    {
      anim = GetComponent<Animator>();
      collider = GetComponent<BoxCollider2D>();
      MovementState state;
    }

    // Update is called once per frame
    void Update()
    {
      AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
      if(currentState.IsName("Electric_Plant_Maintain"))
      {
        lightOff.SetActive(false);
        lightOn.SetActive(true);

      }else
      {
        lightOff.SetActive(true);
        lightOn.SetActive(false);
      }
      if(Time.time > startTimeOffset)
      {
        startCurrentLoop();
      }
    }

    void startCurrentLoop()
    {
      if(!isTimingIdle)
      {
        startTimeIdle = Time.time;
        isTimingIdle = true;
        collider.enabled = false;
      }

      if((Time.time - startTimeIdle > timeIdle) && !isTimingMaintain)
      {
        anim.SetTrigger("start"); 
        startTimeMaintain = Time.time;
        Debug.Log(startTimeMaintain);
        isTimingMaintain = true;
        collider.enabled = true;
      }

      if((Time.time - startTimeMaintain > timeMaintain) && isTimingMaintain)
      {
        anim.SetTrigger("end");
        isTimingIdle = false;
        isTimingMaintain = false;
      }
    }

}
