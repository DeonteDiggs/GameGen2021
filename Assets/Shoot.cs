using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    //calling the input manager
    private Gamegensail gamegensail;

    // variables used for shooting logic
    public float damage = 50f;
    public float range = 100f;
    public GameObject ship;

    private void Awake()
    {
        gamegensail = new Gamegensail();
        gamegensail.Player.Shoot.performed += (context) => ShootRocks(context);
    }


    void ShootRocks(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(ship.transform.position, ship.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Obstacle obstacle = hit.collider.GetComponentInParent<Obstacle>();
            Debug.Log(hit.collider.tag);
            Debug.Log(obstacle);
            if (obstacle)
            {
                Debug.Log("Shooting -----------------------------");
                obstacle.TakeDamage(damage);
            }          
        }
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        gamegensail.Enable();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        gamegensail.Disable();
    }
}
