using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TriggerLoadScene : MonoBehaviour
{
    public string SceneName;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "boat")
        {
            SceneManager.LoadScene(SceneName); // loads scene When player enter the trigger collider
        }
    }
}
