using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_PC_Look : MonoBehaviour
{
    private GameObject go_PCcamera;
    private Rigidbody rb_PC;
    [SerializeField] private float fl_lookSpeed = 3;
    private Vector3 v3_lookDirection = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        go_PCcamera = GameObject.FindGameObjectWithTag("MainCamera");
        rb_PC = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        getLookDirection();
        look();

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
        rb_PC.transform.eulerAngles -= new Vector3(0, v3_lookDirection.y, 0);

        //Vertical looking rotates the camera
        go_PCcamera.transform.eulerAngles -= new Vector3(v3_lookDirection.x, 0, 0);
    }
}
