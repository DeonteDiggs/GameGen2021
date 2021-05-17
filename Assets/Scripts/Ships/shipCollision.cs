using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles collisions for the ships.
public class shipCollision : MonoBehaviour
{

    public GameObject explosion;

    void OnCollisionEnter(Collision hit) 
    {
        if (hit.gameObject.tag == "hazard") 
        {
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            // Remove itself from the ship list in gameplay manager
            gameplayManager.Instance.ships.Remove(gameObject);        
            Destroy(gameObject);
            Destroy(expl, 3);
        }
    }

    void OnDestroy() 
    {
        
    }
}
