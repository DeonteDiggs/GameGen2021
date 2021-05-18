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
    public InGameUIHandler gamehandler;

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
            Destroy(explosion, particle.duration);
            gamehandler.ChangeShipCount();
        }
    }

    void OnDestroy()
    {

    }
}
