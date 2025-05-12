using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_DetectPC : MonoBehaviour
{
    private ZZ_AimAndShoot turretScript;

    // Start is called before the first frame update
    void Start()
    {
        turretScript = GetComponentInChildren<ZZ_AimAndShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Object Entered Trigger");

        if (other.tag == "Player")
        {
            turretScript.SetPCInRange(true);
            Debug.Log("PC Entered Trigger");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Object Exited Trigger");

        if (other.tag == "Player")
        {
            turretScript.SetPCInRange(false);
            Debug.Log("PC Exited Trigger");
        }
    }
}
