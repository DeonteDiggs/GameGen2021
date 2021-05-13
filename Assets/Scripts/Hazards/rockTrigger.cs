using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rockTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision hit) {
        if (hit.collider.tag == "boat") {
            Debug.Log("Removing ship");
            Destroy(hit.gameObject);

            //check if 1 or more ships still exist in scene.
            //If not, reload.

            GameObject[] remainingBoatsArray = GameObject.FindGameObjectsWithTag("boat");
            Debug.Log(remainingBoatsArray.Length);

            if (remainingBoatsArray.Length == 1) {
                Debug.Log("Reloading Scene");
                SceneManager.LoadScene("SampleScene");     
            } 
        }
    }
}
