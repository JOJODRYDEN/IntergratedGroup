using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KR_Hurt : MonoBehaviour
{
    public int in_damage = 1;
    public bool bl_destroyOnHit = false;

    private void OnTriggerEnter(Collider _cl_touching) {

        //if the collision is tagged as a player
        if (_cl_touching.gameObject.CompareTag("Player")) {
            //Debug.Log("Touched Player");

            //send damage and destroy if required
            _cl_touching.gameObject.SendMessage("ApplyDamage", in_damage, SendMessageOptions.RequireReceiver);
            if (bl_destroyOnHit) Destroy(gameObject);
        }
    }
}
