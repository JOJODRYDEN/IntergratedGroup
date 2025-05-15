//=============================================================
// requestsUntilToggle is the numebr of times RequestToggle has
// to be called in order to change objToToggle active or false
// depending on what toggleActive is set to.
//=============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_RemoteEnableObject : MonoBehaviour
{
    [SerializeField] private int requestsUntilToggle = 1; //how many times does set active have to be called before isEnabled is changed

    [SerializeField] private GameObject objToToggle;

    [SerializeField] private bool toggleActive;
    [SerializeField] private bool enableOnStart;

    // Start is called before the first frame update
    void Start()
    {
        objToToggle.SetActive(enableOnStart);

    }

    // Update is called once per frame
    void Update()
    {
        if (requestsUntilToggle <= 0)
        {
            objToToggle.SetActive(toggleActive);

        }

    }

    //-----------------------------

    public void RequestToggle()
    {
        requestsUntilToggle--;
    }
}
