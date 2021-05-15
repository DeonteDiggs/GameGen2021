//Taken from a tutorial online

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floater : MonoBehaviour
{
    [Tooltip("Add the rigidbody component of the object that requires floating here.")]
    public Rigidbody rigidbody;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public int floaterCount = 1;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        rigidbody.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

        float waveHeight = waveManager.instance.getWaveHeight(transform.position.y);

        if( transform.position.y < waveHeight ){
            float displacementMultiplier = Mathf.Clamp01(waveHeight - transform.position.y / depthBeforeSubmerged)*displacementAmount;
            rigidbody.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y)*displacementMultiplier, 0f), ForceMode.Acceleration);
            rigidbody.AddForce(displacementMultiplier * -rigidbody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigidbody.AddTorque(displacementMultiplier * -rigidbody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
