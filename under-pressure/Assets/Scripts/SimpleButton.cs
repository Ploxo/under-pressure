using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleButton : MonoBehaviour
{
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
        interactable.enabled = value;
    }
}
