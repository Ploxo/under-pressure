using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Button : MonoBehaviour
{
    [SerializeField] Light selfLight;
    [SerializeField] ButtonInteractable interactable;


    private void Start()
    {
        SetButtonReady(true);
    }

    // If true, will allow the interactable to interact and fire events; if false it is static
    public void SetButtonReady(bool value)
    {
        selfLight.enabled = value;
        interactable.enabled = value;
    }

    // Triggered by the interactable press event
    public void Activate()
    {
        
    }
}
