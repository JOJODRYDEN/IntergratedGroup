using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KR_HealthMan : MonoBehaviour
{
    public int in_healthMax = 3;
    public int in_health = 3;
    private Vector3 v3_spawnPoint;

    void Start() {

        //on startup set the current location as a checkpoint
        v3_spawnPoint = gameObject.transform.position;

    }

    public void ApplyDamage(int damage) {
        //this method should be called via a sendMessage

        //Debug.Log("Damage Message Caught.");
        in_health -= damage;

        //cap health when healing
        if (in_health > in_healthMax) in_health = in_healthMax;

        if (in_health <= 0) Die();
    }

    public void SetCheckpoint(Vector3 newSpawn) {
        //this method should be called via a sendMessage

        //Debug.Log("Checkpoint Set.");
        v3_spawnPoint = newSpawn;

        //reset health on touching a checkpoint
        in_health = in_healthMax;
    }

    public void Die() {
        //Debug.Log("Death Called.");

        in_health = in_healthMax;

        //reset position to last checkpoint
        gameObject.transform.position = v3_spawnPoint;

    }
}
