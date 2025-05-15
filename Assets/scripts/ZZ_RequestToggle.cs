using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_RequestToggle : MonoBehaviour
{
    [SerializeField] private ZZ_RemoteEnableObject objToToggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        objToToggle.RequestToggle();
        gameObject.SetActive(false);
    }
}
