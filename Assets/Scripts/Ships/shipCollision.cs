using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles collisions for the ships.
public class shipCollision : MonoBehaviour
{
    public GameObject exp;

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "hazard")
        {
            GameObject explosion = Instantiate(exp, transform.position, transform.rotation);
            ParticleSystem.MainModule particle = explosion.GetComponent<ParticleSystem>().main;
            // Remove itself from the ship list in gameplay manager
            gameplayManager.Instance.ships.Remove(gameObject);
            Destroy(gameObject);
            Destroy(explosion, particle.duration);
        }
    }

    void OnDestroy()
    {

    }
}
