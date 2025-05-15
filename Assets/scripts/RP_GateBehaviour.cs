using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class RP_GateBehaviour : MonoBehaviour
{
    public float timer = 10;


    void Start()
    {
        
    }

   
    void Update()
    {
        
    }


    public void OpenGate()
    {
        transform.position -= new Vector3(0f, 1, 0f) * Time.deltaTime;
    }


    public void CloseGate()
    {
       if (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.position += new Vector3(0f, 1, 0f) * Time.deltaTime;
        }
        else
        {
            return;
        }
       
    }

    public void IntantOpenGate()
    {
        gameObject.SetActive(false);

    }


}
