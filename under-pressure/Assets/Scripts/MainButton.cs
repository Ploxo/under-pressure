using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main button for the ship
public class MainButton : MonoBehaviour
{
    [SerializeField] Light selfLight;
    [SerializeField] ButtonInteractable interactable;


    private void Start()
    {
        SetButtonReady(true);
    }

    private void OnEnable()
    {
        interactable.OnPress.AddListener(Activate);
    }

    private void OnDisable()
    {
        interactable.OnPress.RemoveListener(Activate);
    }

    public void SetButtonReady(bool value)
    {
        selfLight.enabled = value;
        interactable.enabled = value;
    }

    public void Activate()
    {
        selfLight.enabled = false;
    }
}
