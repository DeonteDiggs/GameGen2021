using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floater : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        float waveHeight = waveManager.instance.getWaveHeight(transform.position.x);

        if( transform.position.y < waveHeight ){
            float displacementMultiplier = Mathf.Clamp01(waveHeight - transform.position.y / depthBeforeSubmerged)*displacementAmount;
            rigidbody.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y)*displacementMultiplier, 0f), ForceMode.Acceleration);
        }
    }
}
