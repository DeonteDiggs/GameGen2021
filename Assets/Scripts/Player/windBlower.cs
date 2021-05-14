using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
     * This class handles code for the wind blower object, including
     * its movement code, its code for adding force to a specific object
     * (specifically objects with the 'boat' tag), and code for getting input.
     * obviously incredibly unpolished/disorganized.
     * apologies if i've violated C# code-style guidelines, I come from a Java background. 
*/
public class windBlower : MonoBehaviour
{
    // Variable that stores our boat object obtained by raycasting.
    public Rigidbody boatRb;

    // Object that windBlower should rotate around
    public GameObject targetObject;

    // Object that we should instantiate in FireProjectile()
    public GameObject playerProjectile;

    // How fast should the play rotate around targetObject?
    public float moveSpeed;

    // What direction will they move in?
    public float direction;

    // How much force do we apply to boat?
    public float boatForce;

    // Update is called once per frame
    void FixedUpdate()
    {
        //checkForBoat();
        transform.RotateAround(targetObject.transform.position, Vector3.up, ((moveSpeed * direction) * Time.deltaTime));
    }

    //called by button press; sends a raycast to check for boat object.
    //If we find boat object, store it's rigidbody in boat variable and applyForce().
    void CheckForBoat()
    {
        // Raycast collider.
        // TODO: Raycast should ignore all non-boat objects.
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null)
            {
                Debug.Log("WE GOT ONE!");
                if (hit.collider.gameObject.tag == "boat")
                {
                    Debug.Log("boats bboats boats");
                    boatRb = hit.rigidbody;
                    ApplyForce();
                }
            }
        }
    }

    void FireProjectile()
    {

    }

    //==Input Code==
    public void Blow(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
        CheckForBoat();
    }

    // Move around a radius
    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<float>();
    }

    public void SwitchShips(InputAction.CallbackContext context)
    {
        // Get value from context, round it up to nearest whole (-1 or 1) just for safety
        Debug.Log(context);

        // Use that value to go back and forth in the list stored in gameplayManager
        // We should probably use a function in gameplayManager.
        targetObject tO = transform.parent.GetComponent<targetObject>();
        tO.SwitchTargetShip(context.ReadValue<float>());
    }

    // Get input to fire a projectile
    public void Shoot(InputAction.CallbackContext context)
    {
        Debug.Log("Shoot!");
        FireProjectile();
    }

    //Applies the boat force to our captured boat object.
    void ApplyForce()
    {
        boatRb.AddForce(transform.forward * boatForce);
    }
}
