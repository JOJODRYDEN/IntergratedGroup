using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_RaycastTickDmg : MonoBehaviour
{
    [SerializeField] private KR_HealthMan healthScript;
    [SerializeField] private int int_dmgPerTick = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit collision;

        Debug.DrawRay(transform.position, transform.up * 30, Color.green);
        if (Physics.Raycast(transform.position, transform.up * 30, out collision))
        {
            //Debug.Log("Ray Collision");
            if (collision.transform.CompareTag("Player"))
            {
                //Debug.Log("hit PC");
                healthScript.ApplyDamage(int_dmgPerTick);
            }
        }
    }
}
