using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_PC_Movement : MonoBehaviour
{
    private Rigidbody rb_PC;
    [SerializeField] private float fl_movementSpeed = 5f;
    [SerializeField] private float fl_jumpSpeed = 5f;
    [SerializeField] private float fl_slowdownSpeed = 5f;
    [SerializeField] private float fl_maxGroundedSpeed = 15f;
    [SerializeField] private float fl_stopSpeed = 3f;
    [SerializeField] private bool bl_canMoveMidair = true;
    private bool bl_grounded = true;
    private Vector3 v3_movementDirection = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb_PC = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        takeInput();
    }

    void FixedUpdate()
    {
        //if you can move in mid-air, allow movement regardless of being grounded
        if (bl_canMoveMidair)
        {
            move();
        }
        else
        {
            if (bl_grounded)
            {
                move();
            }
        }

        //Reduce speed if grounded and not moving
        //if (bl_grounded && v3_movementDirection == new Vector3(0, 0, 0))
        //{
        //    slowDown();
        //}

        slowDown();

        limitSpeed();
    }

    //---------------------------------

    //sets v3_movementDirection to normalised inputs
    private void takeInput()
    {
        v3_movementDirection = new Vector3(
            Input.GetAxis("Horizontal"), 
            0, 
            Input.GetAxis("Vertical")
            ).normalized;
    }

    //adds v3_movementDirection to velocity of rb_PC
    private void move()
    {
        rb_PC.velocity += v3_movementDirection * fl_movementSpeed;
    }

    //gradualy reduces rb_PC.velocity to 0
    private void slowDown()
    {
        //decrease if above stop speed
        if (rb_PC.velocity.x > fl_stopSpeed)
        {
            rb_PC.velocity -= new Vector3(fl_slowdownSpeed, 0, 0);
        }
        if (rb_PC.velocity.z > fl_stopSpeed)
        {
            rb_PC.velocity -= new Vector3(0, 0, fl_slowdownSpeed);
        }

        //increase if below negative stop speed
        if (rb_PC.velocity.x < -fl_stopSpeed)
        {
            rb_PC.velocity += new Vector3(fl_slowdownSpeed, 0, 0);
        }
        if (rb_PC.velocity.z < -fl_stopSpeed)
        {
            rb_PC.velocity += new Vector3(0, 0, fl_slowdownSpeed);
        }

        //set to 0 if within positive and negative stop speed
        if (rb_PC.velocity.x > -fl_slowdownSpeed && rb_PC.velocity.x < fl_slowdownSpeed)
        {
            rb_PC.velocity = new Vector3(0, rb_PC.velocity.y, rb_PC.velocity.z);
        }
        if (rb_PC.velocity.z > -fl_slowdownSpeed && rb_PC.velocity.z < fl_slowdownSpeed)
        {
            rb_PC.velocity = new Vector3(rb_PC.velocity.x, rb_PC.velocity.y, 0);
        }
    }

    //limits horizontal rb_PC.velocity to v3_maxGroundSpeed
    private void limitSpeed()
    {
        if (rb_PC.velocity.x > fl_maxGroundedSpeed)
        {
            rb_PC.velocity = new Vector3(fl_maxGroundedSpeed, rb_PC.velocity.y, rb_PC.velocity.z);
        }
        if (rb_PC.velocity.z > fl_maxGroundedSpeed)
        {
            rb_PC.velocity = new Vector3(rb_PC.velocity.x, rb_PC.velocity.y, fl_maxGroundedSpeed);
        }


        if (rb_PC.velocity.x < -fl_maxGroundedSpeed)
        {
            rb_PC.velocity = new Vector3(-fl_maxGroundedSpeed, rb_PC.velocity.y, rb_PC.velocity.z);
        }
        if (rb_PC.velocity.z < -fl_maxGroundedSpeed)
        {
            rb_PC.velocity = new Vector3(rb_PC.velocity.x, rb_PC.velocity.y, -fl_maxGroundedSpeed);
        }
    }
}