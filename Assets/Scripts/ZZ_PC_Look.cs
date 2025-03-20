using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_PC_Look : MonoBehaviour
{
    private ZZ_GravityInvertion scr_gravityInversion;
    private GameObject go_PCcamera;
    private Rigidbody rb_PC;
    private Vector3 v3_lookDirection = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        go_PCcamera = GameObject.FindGameObjectWithTag("MainCamera");
        rb_PC = GetComponentInParent<Rigidbody>();
        scr_gravityInversion = GetComponentInParent<ZZ_GravityInvertion>();
    }

    // Update is called once per frame
    void Update()
    {
        getLookDirection();
        look();
        limitXLookAngle();

        //Debug.Log(rb_PC.transform.localEulerAngles.y);
        //Debug.Log("Look Direction:" + v3_lookDirection);
    }

    //----------------------------------

    //takes mouse input, setting v3_lookDirection
    void getLookDirection()
    {
        v3_lookDirection = -new Vector3(
            -Input.GetAxis("Mouse Y"),
            Input.GetAxis("Mouse X"),
            0);
    }

    //applies mouse input to rotate character and camera
    void look()
    {
        //horizontal looking rotates the whole character
        if (!scr_gravityInversion.getFlipped())
        {
            rb_PC.transform.eulerAngles -= new Vector3(0, v3_lookDirection.y, 0);
        }
        else
        {
            //if flipped, invert horizontal looking
            rb_PC.transform.eulerAngles += new Vector3(0, v3_lookDirection.y, 0);
        }

        //Vertical looking rotates the camera
        go_PCcamera.transform.localEulerAngles -= new Vector3(v3_lookDirection.x, 0, 0);
    }

    //limits vertical camera movement as to prevent incorrect camera rotation when rotating PC rigidbody
    void limitXLookAngle()
    {
        if (go_PCcamera.transform.localEulerAngles.x <= 280 && go_PCcamera.transform.localEulerAngles.x > 90)
        {
            go_PCcamera.transform.localEulerAngles = new Vector3(280, go_PCcamera.transform.localEulerAngles.y, go_PCcamera.transform.localEulerAngles.z);
        }

        if (go_PCcamera.transform.localEulerAngles.x >= 80 && go_PCcamera.transform.localEulerAngles.x < 270)
        {
            go_PCcamera.transform.localEulerAngles = new Vector3(80, go_PCcamera.transform.localEulerAngles.y, go_PCcamera.transform.localEulerAngles.z);
        }
    }
}
