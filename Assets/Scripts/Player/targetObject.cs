using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetObject : MonoBehaviour
{
    public GameObject targetBoat;

    public int shipIndex = 0;

    void Start() {
        /*
        GameObject gameManager = GameObject.Find("gameManagement");

        if (gameManager == null) {
            Debug.Log("Error, couldn't find gameplayManager");
        } else {
            gameManageScript = gameManager.GetComponent<gameplayManager>();
        }
        */

        targetBoat = gameplayManager.Instance.ships[shipIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (targetBoat != null) {        
            transform.position = targetBoat.transform.position;
        } else if (targetBoat == null) {
            targetBoat = GameObject.FindGameObjectWithTag("boat"); 
        }

        //Debug.Log("ships count: " + gameplayManager.Instance.ships.Count);
    }

    // Switch between ships.
    // value is a positive or negative number. 
    public void SwitchTargetShip(float value) {
        //if value is less than 0, its -1.
        //if value is greater than 0, its +1.

        shipIndex += (int)value;

        //Debug.Log(shipIndex);

        int currentShipCount = gameplayManager.Instance.ships.Count;

        if (shipIndex < 0) {
            //targetBoat = gameplayManager.Instance.ships[currentShipCount - 1];
            shipIndex = currentShipCount - 1;
        } else if (shipIndex > currentShipCount - 1) {
            //targetBoat = gameplayManager.Instance.ships[0]
            shipIndex = 0;
        } 

        targetBoat = gameplayManager.Instance.ships[shipIndex];
    }
}
