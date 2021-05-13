using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetObject : MonoBehaviour
{
    public GameObject targetBoat;

    private gameplayManager gameManageScript;

    void Start() {
        GameObject gameManager = GameObject.Find("gameManagement");

        if (gameManager == null) {
            Debug.Log("Error, couldn't find gameplayManager");
        } else {
            gameManageScript = gameManager.GetComponent<gameplayManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetBoat != null) {        
            transform.position = targetBoat.transform.position;
        } else if (targetBoat == null) {
            targetBoat = GameObject.FindGameObjectWithTag("boat"); 
        }
    }

    // Switch between ships.
    // value is a positive or negative number. 
    public void SwitchTargetShip(float value) {
        
    }
}
