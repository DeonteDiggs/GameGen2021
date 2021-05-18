using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TriggerLoadScene : MonoBehaviour
{
    public string SceneName1;
    public string SceneName2;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "boat")
        {
            SceneManager.LoadScene(SceneName1); // loads scene When player enter the trigger collider
        }
        if (other.tag == "Finish")
        {
            SceneManager.LoadScene(SceneName2); // loads scene When player enter the trigger collider
        }
    }
}
