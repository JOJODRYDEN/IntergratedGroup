using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JD_LightPuzzleAcessPanel : MonoBehaviour, IInteractable
{
    public GameObject PLayerCamura;
    public GameObject PuzzleCamura;
    public void Interact()
    {
        Debug.Log("player interacted");
        PLayerCamura.SetActive(false);
        PuzzleCamura.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PLayerCamura.SetActive(true);
            PuzzleCamura.SetActive(false);
        }
    }
}
