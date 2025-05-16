using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KR_Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider _cl_touching) {

        //if the collision is tagged as a player
        if (_cl_touching.gameObject.CompareTag("Player")) {
            //Debug.Log("Touched Player");

            //send checkpoint location
            _cl_touching.gameObject.SendMessage("SetCheckpoint", gameObject.transform.position, SendMessageOptions.RequireReceiver);
        }
    }
}
