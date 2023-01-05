using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Example button class that shows how to use the custom ButtonInteractable
public class ExampleButton : MonoBehaviour
{
    [SerializeField] Light selfLight;
    [SerializeField] ButtonInteractable interactable;


    private void Start()
    {
        SetButtonReady(true);
    }

    private void OnEnable()
    {
        // Can be wired up in script like below to a method with any access modifier,
        // or directly on the interactable GameObject's inspector to a public method on this object.
        interactable.OnPress.AddListener(TestPrivate);
    }

    private void OnDisable()
    {
        // Don't forget to unregister
        interactable.OnPress.RemoveListener(TestPrivate);
    }

    // If true, will allow the interactable to interact and fire events; if false it becomes uninteractable
    public void SetButtonReady(bool value)
    {
        selfLight.enabled = value;
        interactable.enabled = value;
    }

    // Triggered by the interactable press event, which is wired up in the Inspector of the interactable GameObject
    public void Activate()
    {
        selfLight.enabled = false;
        // Do other stuff
    }

    // This one's wired up in here instead
    private void TestPrivate()
    {
        // Do stuff
    }
}
