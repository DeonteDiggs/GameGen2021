using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class OnMouseHandler : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] UIHandler uiHandler;

    [SerializeField] Gamegensail gamegensail;

    private void Awake()
    {
        gamegensail = new Gamegensail();
    }

    void OnEnable() {
        gamegensail.UI.Enable();
    }
    void OnDisable() {
        gamegensail.UI.Disable();
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        print(gamegensail.UI.Click.triggered);
        if (gamegensail.UI.Click.triggered)
        {
            uiHandler.DisableControlsMenu();
        }
    }

    
}
