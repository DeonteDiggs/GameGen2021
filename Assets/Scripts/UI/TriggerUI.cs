using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUI : MonoBehaviour
{
    [SerializeField] private Animation mainMenuFadeIn;
    [SerializeField] private Animation titleFadeIn;
    [SerializeField] private Animation titleBackgroundFadeIn;
    public bool isInMainMenu;
    public bool isInCutScene;
   

    [SerializeField] private GameObject EndLevelMenu;
    

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "boat")
        {
            
            if (isInMainMenu)
            {
                StartCoroutine(WaitBeforeTitleFadeIn());
                StartCoroutine(WaitBeforeMenuFadeIn());
            }

            else if (isInCutScene) {
                StartCoroutine(WaitBeforeEndLevelMenu());
                

            }
           
            
        }
    }

    public IEnumerator WaitBeforeMenuFadeIn() {
        
        yield return new WaitForSeconds(4f);
        mainMenuFadeIn.Play();
    }

    public IEnumerator WaitBeforeTitleFadeIn()
    {
        
        yield return new WaitForSeconds(2f);
        titleBackgroundFadeIn.Play();
        titleFadeIn.Play();
    }
    
    public IEnumerator WaitBeforeEndLevelMenu()
    {
        
        yield return new WaitForSeconds(1.5f);
        EndLevelMenu.SetActive(true);
    }
}
