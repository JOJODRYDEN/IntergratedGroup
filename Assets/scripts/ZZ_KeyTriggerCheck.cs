using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_KeyTriggerCheck : MonoBehaviour
{
    [SerializeField] private ZZ_AltarPuzzleGateOpen altarPuzzleScript;
    [SerializeField] private string designatedKeyLight;
    private bool wasTriggered;

    // Start is called before the first frame update
    void Start()
    {
        wasTriggered = false;
}

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision detected");
        if (other.tag == designatedKeyLight && !wasTriggered)
        {
            altarPuzzleScript.IncrementKeyMatchedCount();
            wasTriggered = true;
            Debug.Log("Key Unlocked");
        }
    }
}
