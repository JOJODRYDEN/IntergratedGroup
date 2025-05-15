using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZZ_OpenScene : MonoBehaviour
{
    [SerializeField] private string sceneToOpen;

    public void OpenScene()
    {
        SceneManager.LoadScene(sceneToOpen);

    }//---

    public void QuitGame()
    {
        Application.Quit();

    }//---
}
