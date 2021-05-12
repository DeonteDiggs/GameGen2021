using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class windBlower : MonoBehaviour
{
    /*
     * This class handles code for the wind blower object, including
     * its movement code, its code for adding force to a specific object
     * (specifically objects with the 'boat' tag), and code for getting input.
     * obviously incredibly unpolished/disorganized.
     * apologies if i've violated C# code-style guidelines, I come from a Java background. 
     */

    //variable that stores our boat object obtained by raycasting.
    public Rigidbody boatRb;

    //Object that windBlower should rotate around
    public GameObject targetObject;

    //How fast should the play rotate around targetObject?
    public float moveSpeed;

    //What direction will they move in?
    public float direction;

    //How much force do we apply to boat?
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
        //Raycast collider.
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


    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
        CheckForBoat();
    }

    //Move around a radius
    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<float>();
    }

    //Applies the boat force to our captured boat object.
    void ApplyForce()
    {
        boatRb.AddForce(transform.forward * boatForce);
    }
}
