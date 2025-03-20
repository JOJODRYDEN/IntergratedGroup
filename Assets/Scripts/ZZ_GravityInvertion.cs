using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_GravityInvertion : MonoBehaviour
{
    [SerializeField] private bool bl_flipped = false;
    private bool bl_flipStateBeforeRotation = false;

    [SerializeField] private float fl_timerMax = 5f;
    private float fl_timer;
    private bool bl_timerActive = false;

    [SerializeField] private bool bl_PC = true;
    [SerializeField] private float fl_activationRange = 10f;
    private float fl_gravityPowerCooldownPercentage = 100f;
    private Rigidbody rb_RB;

    // Start is called before the first frame update
    void Start()
    {
        rb_RB = GetComponent<Rigidbody>();

        fl_timer = fl_timerMax;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(fl_timer);

        //if script is on PC
        if (bl_PC)
        {
            //if timer finished, stop decrementing it, and reset it
            if (fl_timer <= 0)
            {
                bl_timerActive = false;
                fl_timer = fl_timerMax;
            }
            //if timer active, decrement it
            if (bl_timerActive)
            {
                fl_timer -= Time.deltaTime;
                rotateAlongZ();
            }

            //can only invert gravity if timer is not active
            if (Input.GetKeyDown("f") && !bl_timerActive)
            {
                bl_timerActive = true;

                //store bl_flipStateBeforeRotation and invert bl_flipped
                bl_flipStateBeforeRotation = bl_flipped;
                if (bl_flipped)
                {
                    bl_flipped = false;
                }
                else
                {
                    bl_flipped = true;
                }
            }
        }
        else
        {//if script is not on PC
            // can only invert gravity if PC in range
            if (Input.GetKeyDown("c") && Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position) <= fl_activationRange)
            {
                //invert bl_flipped
                if (bl_flipped)
                {
                    bl_flipped = false;
                }
                else
                {
                    bl_flipped = true;
                }
            }
        }

        //if flipped, dont use unity's gravity
        if (bl_flipped)
        {
            rb_RB.useGravity = false;
        }
        else 
        {
            rb_RB.useGravity = true;
        }
    }

    void FixedUpdate()
    {
        //if flipped, constantly apply inverted gravity
        if (bl_flipped)
        {
            rb_RB.AddForce(Vector3.up * 9.81f, ForceMode.Acceleration);
        }
    }

    //------------------------------

    //public getter for bl_flipped
    public bool getFlipped()
    {
        return bl_flipped;
    }

    //public getter for fl_gravityPowerCooldownPercentage
    public float getGravityPowerCooldownPercentage()
    {
        return fl_gravityPowerCooldownPercentage;
    }

    //runs every frame when timer is active. calculates percentage of timer completion and uses that to rotate PC over length of timer
    private void rotateAlongZ()
    {
        float timerCompletionPercentage = 100 - ((fl_timer / fl_timerMax) * 100);
        fl_gravityPowerCooldownPercentage = timerCompletionPercentage;
        //calculate what degrees the current timer percentage corresponds to, assuming 180 degrees is 100%.
        float rotateTo = (timerCompletionPercentage / 100) * 180;
        //sometimes it ends on a decimal above 180
        if (rotateTo > 180) rotateTo = 180;

        //match the rigidbody's rotation with the calculated rotation
        if (!bl_flipStateBeforeRotation)
        { 
            rb_RB.transform.eulerAngles = new Vector3(rb_RB.transform.localEulerAngles.x, rb_RB.transform.localEulerAngles.y, rotateTo); 
        }
        else
        {//rotate the other way when rotating from a flipped position
            rb_RB.transform.eulerAngles = new Vector3(rb_RB.transform.localEulerAngles.x, rb_RB.transform.localEulerAngles.y, 180 - rotateTo);
        }

        //Debug.Log(rotateTo);
    }
}
