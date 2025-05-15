using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_PasscodePuzzle : MonoBehaviour
{
    [SerializeField] private int PwdFrstNumber = 3;
    [SerializeField] private int PswdScndNumber = 3;
    [SerializeField] private int PswdThirNumber = 3;
    [SerializeField] private int PswdFourNumber = 3;
    [SerializeField] public RP_GateBehaviour OpenGate;

    private int inputCounter;

    private int[] passcode = {1, 9, 8, 7};
    private int[] inputCode = { -1, -1, -1, -1};

    private string debugMsg;

    // Start is called before the first frame update
    void Start()
    {
        inputCounter = 0;

        passcode[0] = PwdFrstNumber;
        passcode[1] = PswdScndNumber;
        passcode[2] = PswdThirNumber;
        passcode[3] = PswdFourNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputCounter >= 4) 
        {
            if (passcode[0] == inputCode[0])
            {
                if (passcode[1] == inputCode[1])
                {
                    if (passcode[2] == inputCode[2])
                    {
                        if (passcode[3] == inputCode[3])
                        {
                            Debug.Log("Puzzle Solved!");
                            OpenGate.OpenGate();
                        }
                        else resetInputCode();
                    }
                    else resetInputCode();
                }
                else resetInputCode();
            }
            else resetInputCode();
        }
    }

    //--------------------------------------

    //resets the puzzle when wrong code is input
    private void resetInputCode()
    {
        Debug.Log("Puzzle failed! Try again...");

        inputCounter = 0;

        inputCode[0] = -1;
        inputCode[1] = -1;
        inputCode[2] = -1;
        inputCode[3] = -1;
    }

    //to be used by other scripts
    public void SetInputCode(int input)
    {
        debugMsg = "Count: " + inputCounter;

        inputCode[inputCounter] = input;
        inputCounter++;

        debugMsg += ", First: " + inputCode[0];
        debugMsg += ", Second: " + inputCode[1];
        debugMsg += ", Third: " + inputCode[2];
        debugMsg += ", Fourth: " + inputCode[3];
        Debug.Log(debugMsg);
    }
}
