using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles collisions for the ships.
public class shipCollision : MonoBehaviour
{
    private AudioSource audio;
    private GameObject camera;
    public AudioClip audioclip;
    public GameObject exp;

    void OnCollisionEnter(Collision hit) 
    {
    
        if (hit.gameObject.tag == "hazard") 
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera");
            audio = camera.GetComponent<AudioSource>();
            audio.PlayOneShot(audioclip, 1);
            GameObject explosion = Instantiate(exp, transform.position, transform.rotation);
            ParticleSystem.MainModule particle = explosion.GetComponent<ParticleSystem>().main;
            // Remove itself from the ship list in gameplay manager
            gameplayManager.Instance.ships.Remove(gameObject);
            Destroy(gameObject);
<<<<<<< HEAD
            Destroy(explosion, particle.duration);
=======

            
>>>>>>> 27947b722f42dfc8470911a3e3dfe9707a53b260
        }
    }

    void OnDestroy()
    {

    }
}
