using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_AltarPuzzleGateOpen : MonoBehaviour
{
    [SerializeField] private RP_GateBehaviour gateToOpen;

    [SerializeField] private int noOfKeysToMatch = 3;
    private int keyMatchedCount= 0;

    // Start is called before the first frame update
    void Start()
    {
        keyMatchedCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(keyMatchedCount >= noOfKeysToMatch)
        {
            gateToOpen.IntantOpenGate();
        }
    }

    //-----------------------

    //this is called by key trigger check
    public void IncrementKeyMatchedCount()
    {
        keyMatchedCount++;
    }
}
