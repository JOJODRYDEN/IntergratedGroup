using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JD_KeyMovement : MonoBehaviour
{
    public float speed = 6f;

    void Update()
    {
    
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized * speed * Time.deltaTime;
        transform.position += moveDirection;
    }

}

