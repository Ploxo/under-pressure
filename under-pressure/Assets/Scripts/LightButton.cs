using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButton : MonoBehaviour
{
    [SerializeField] Light selfLight;
    [SerializeField] ButtonInteractable interactable;


    private void Start()
    {
        SetButtonReady(true);
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void SetButtonReady(bool value)
    {
        SetLight(value);
        interactable.enabled = value;
    }

    public void SetLight(bool value)
    {
        selfLight.enabled = value;
    }
}
