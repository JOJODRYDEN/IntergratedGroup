using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JD_LightPuzzleSelecter : MonoBehaviour
{
    public GameObject key1;
    public GameObject key2;
    public GameObject key3;

    public GameObject key1Light;
    public GameObject key2Light;
    public GameObject key3Light;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {

            key1Light.SetActive(true);
            key2Light.SetActive(false);
            key3Light.SetActive(false);

            key1.GetComponent<JD_KeyMovement>().enabled = true;
            key2.GetComponent<JD_KeyMovement>().enabled = false;
            key3.GetComponent<JD_KeyMovement>().enabled = false;


  
        }

        if (Input.GetKeyDown(KeyCode.X))
        {

            key1Light.SetActive(false);
            key2Light.SetActive(true);
            key3Light.SetActive(false);


            key2.GetComponent<JD_KeyMovement>().enabled = true;
            key1.GetComponent<JD_KeyMovement>().enabled = false;
            key3.GetComponent<JD_KeyMovement>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        { 

            key1Light.SetActive(false);
            key2Light.SetActive(false);
            key3Light.SetActive(true);

            key3.GetComponent<JD_KeyMovement>().enabled = true;
            key2.GetComponent<JD_KeyMovement>().enabled = false;
            key1.GetComponent<JD_KeyMovement>().enabled = false;
        }


    }



}

