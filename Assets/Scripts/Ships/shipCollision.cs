using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles collisions for the ships.
public class shipCollision : MonoBehaviour
{
    [SerializeField] private InGameUIHandler inGameUIHandler;
    void OnCollisionEnter(Collision hit) 
    {
        if (hit.gameObject.tag == "hazard") 
        {
            // Remove itself from the ship list in gameplay manager
            gameplayManager.Instance.ships.Remove(gameObject);
            inGameUIHandler.ChangeShipCount();
            Destroy(gameObject);
        }
    }

    void OnDestroy() 
    {
        
    }
}
