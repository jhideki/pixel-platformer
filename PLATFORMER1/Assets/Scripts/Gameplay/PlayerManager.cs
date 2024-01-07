using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager: MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerManager instance;
    public enum PlayerState {mc,companion};
    PlayerState currentState;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentState = PlayerState.mc;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(currentState == PlayerState.mc)
            {
                currentState = PlayerState.companion;
            }
            else
            {
                currentState = PlayerState.mc;
            }

        }
    }

    public PlayerState GetCurrentState()
    {
        return currentState;
    }


}
