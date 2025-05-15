using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RP_SceneChange : MonoBehaviour
 {
    [SerializeField]
    public float SceneLevel;
    public bool Touching;

    void Start()
    {
        Touching = false;  
    }
    public void OnTriggerEnter(Collider other)
    {
        
        if (SceneLevel == 0)
        {
            Touching = true;
            if (Touching == true)
            {
                SceneManager.LoadScene(1);
                SceneLevel += 1;
                Touching = false;
            }

        }
      
        if (SceneLevel == 1)
        {
            Touching = true;
            if (Touching == true)
            {
                SceneManager.LoadScene(2);
                Touching = false;
            }
           
            
        }
        

    }
}
