using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{   
     void Interact();
}

public class JD_PC_interact : MonoBehaviour
{
    public Transform Interact;
    public float interactRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(Interact.position, Interact.forward);
            if(Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactobj))
                {
                    interactobj.Interact();
                }
            }
        }
        
    }
}
