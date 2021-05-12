using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetObject : MonoBehaviour
{
    public GameObject targetBoat;

    // Update is called once per frame
    void Update()
    {
        if (targetBoat != null) {        
            transform.position = targetBoat.transform.position;
        } else if (targetBoat == null) {
            targetBoat = GameObject.FindGameObjectWithTag("boat"); 
        }
    }
}
