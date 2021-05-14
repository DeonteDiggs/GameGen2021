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
    }

    // Switch between ships.
    // value is a positive or negative number. 
    public void SwitchTargetShip(float value) {
        //if value is less than 0, its -1.
        //if value is greater than 0, its +1.

         Debug.Log(value);
         
         int intendedShipIndex = shipIndex + (int)value;

         if (intendedShipIndex < 0) {
             shipIndex = (gameplayManager.Instance.ships.Count) - 1;
         } else if (intendedShipIndex > (gameplayManager.Instance.ships.Count) - 1) {
             shipIndex = 0;
         } else {
             shipIndex = intendedShipIndex;
         }

         targetBoat = gameplayManager.Instance.ships[shipIndex];
    }
}
