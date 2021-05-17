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
    //calling the input manager
    private Gamegensail gamegensail;

    // Variable that stores our boat object obtained by raycasting.
    public Rigidbody boatRb;

    // Object that windBlower should rotate around
    public GameObject targetGameObject;

    // Object that we should instantiate in FireProjectile()
    public GameObject playerProjectile;

    public targetObject targetObjectScript;

    // How fast should the play rotate around targetObject?
    public float moveSpeed;

    // What direction will they move in?
    public float direction;

    // How much force do we apply to boat?
    public float boatForce;

    private bool blowEnabled;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        gamegensail = new Gamegensail();
        gamegensail.Player.start_blow.performed += (context) => startBlow();
        gamegensail.Player.stop_blow.performed += context1 => stopBlow();
    }

    void Start()
    {
        targetGameObject = transform.parent.gameObject;
        targetObjectScript = targetGameObject.GetComponent<targetObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //checkForBoat();
        transform.RotateAround(targetGameObject.transform.position, Vector3.up, ((moveSpeed * direction) * Time.deltaTime));

        if(blowEnabled){
            Debug.Log("Fire!");
            CheckForBoat();
        }
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
    public void startBlow()
    {
        Debug.Log("Start to blow!!");
        blowEnabled = true;
    }

    public void stopBlow()
    {
        Debug.Log("Stop to blow!!");
        blowEnabled = false;
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
        

        if (context.performed) {
            targetObjectScript.SwitchTargetShip(context.ReadValue<float>());
        }
        
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
