using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonBall : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        StartCoroutine(DestroyCountdown(10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Destroy on collision
    //If colliding with hazards, also destroy those.
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "hazard") {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        //Destroy(gameObject);
    }


    private IEnumerator DestroyCountdown(int count)
    {
        for (int i = 0; i <= count; i++) {
            Debug.Log(i + " seconds have passed");
            yield return new WaitForSeconds(1.0f);
        }
        Destroy(gameObject);
    }
}
