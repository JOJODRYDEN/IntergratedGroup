using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RP_PressurePad : MonoBehaviour
{
    [SerializeField]
    public RP_GateBehaviour GateScript;

    [SerializeField]
    public float timeRemaining = 10;
    [SerializeField]
    public bool timerIsRunning = false;
    [SerializeField]
    public bool gateActivation;
  
    public void OnTriggerEnter(Collider other)
    {
        timerIsRunning = true;
        
    }

 
    void Update()
    {
        if (timerIsRunning)
        {
            GateScript.OpenGate();
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                gateActivation = false;

            }
            else
            {
                gateActivation = true;
                Debug.Log("Times Up!");
                timeRemaining = 0;
                timerIsRunning = false;
                timeRemaining = 10;
            }

        }
        if (gateActivation == true)
        {
            GateScript.CloseGate();
        }
       
    }
}
