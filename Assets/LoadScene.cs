using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public string SceneName;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            print("hi");
            SceneManager.LoadScene(SceneName); // loads scene When player enter the trigger collider
        }
    }
}
