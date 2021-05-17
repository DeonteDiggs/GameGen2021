using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles collisions for the ships.
public class shipCollision : MonoBehaviour
{
    private AudioSource audio;
    private GameObject camera;
    public AudioClip audioclip;

    void OnCollisionEnter(Collision hit) 
    {
        audio = GetComponent<AudioSource>();
    
        if (hit.gameObject.tag == "hazard") 
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera");
            audio = camera.GetComponent<AudioSource>();
            audio.PlayOneShot(audioclip, 1);
            // Remove itself from the ship list in gameplay manager
            gameplayManager.Instance.ships.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    void OnDestroy() 
    {
        
    }
}
