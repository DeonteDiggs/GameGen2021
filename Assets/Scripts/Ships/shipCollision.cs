using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles collisions for the ships.
public class shipCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision hit) 
    {
        if (hit.gameObject.tag == "hazard") 
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy() 
    {
        // Remove itself from the ship list in gameplay manager
        gameplayManager.Instance.ships.Remove(gameObject);

    }
}
