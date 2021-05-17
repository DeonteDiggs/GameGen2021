using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUI : MonoBehaviour
{
    [SerializeField] private Animation mainMenuFadeIn;
    [SerializeField] private Animation titleFadeIn;
    
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "boat")
        {
            StartCoroutine(WaitBeforeTitleFadeIn());
            StartCoroutine(WaitBeforeMenuFadeIn());
            
        }
    }

    public IEnumerator WaitBeforeMenuFadeIn() {
        
        yield return new WaitForSeconds(4f);
        mainMenuFadeIn.Play();
    }

    public IEnumerator WaitBeforeTitleFadeIn()
    {
        
        yield return new WaitForSeconds(2f);
        titleFadeIn.Play();
    }
}
