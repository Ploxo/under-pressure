using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonInteractable : XRBaseInteractable
{
    public UnityEvent OnPress = null;

    [SerializeField, Range(0f, 1f), Tooltip("The activation happens at this percentage height of the collider")] 
    private float activationThreshold = 0.5f;

    private float minY = 0f;
    private float maxY = 0f;
    private bool wasPressedLastFrame = false;

    private IXRHoverInteractor hoverInteractor = null;
    private float previousHandHeight = 0f;

    private Collider coll;


    protected override void Awake()
    {
        base.Awake();

        hoverEntered.AddListener(StartPress);
        hoverExited.AddListener(EndPress);
    }

    protected override void OnDestroy()
    {
        // base is empty here, but green lines are annoying
        base.OnDestroy();

        hoverEntered.RemoveListener(StartPress);
        hoverExited.RemoveListener(EndPress);
    }

    private void Start()
    {
        coll = GetComponent<Collider>();
        SetMinMax();
    }

    // Height range is based on collider
    private void SetMinMax()
    {
        minY = transform.localPosition.y - (coll.bounds.size.y * 0.5f);
        maxY = transform.localPosition.y;
    }

    // Runs on interactable hover enter and sets the current interactor and position
    private void StartPress(HoverEnterEventArgs args)
    {
        hoverInteractor = args.interactorObject;
        previousHandHeight = GetLocalYPosition(args.interactorObject.transform.position);
    }

    // Runs on interactable hover exit and resets everything
    private void EndPress(HoverExitEventArgs args)
    {
        hoverInteractor = null;
        previousHandHeight = 0f;

        wasPressedLastFrame = false;
        SetYPosition(maxY);
    }

    // Interactable version of Update(); runs every frame?
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        // Do nothing if nothing's nearby, otherwise update position of the button
        if (hoverInteractor != null)
        {
            // Take the 
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;

            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);

            CheckPress();
        }
    }

    // Local y position allows any orientation of the button to work
    private float GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(position);

        return localPosition.y;
    }

    // Just clamps the button height to the min/max
    private void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, minY, maxY);
        transform.localPosition = newPosition;
    }

    private void CheckPress()
    {
        bool isPressed = IsInPosition();

        // Detect a positive change in activation state
        if (isPressed && !wasPressedLastFrame)
        {
            Debug.Log("Detected press");
            OnPress.Invoke();
        }

        // This frame becomes last frame
        wasPressedLastFrame = isPressed;
    }

    private bool IsInPosition()
    {
        // Check if value remains the same after clamping to activation range; if true, it was in range
        float inRange = Mathf.Clamp(transform.localPosition.y, minY, minY + activationThreshold);

        Debug.Log("Is in range: " + (transform.localPosition.y == inRange));

        return transform.localPosition.y == inRange;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 localPosition = transform.localPosition;
        Vector3 size = coll.bounds.size;
        size.y = 0.01f * size.y;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(localPosition.x, minY, localPosition.z), size);
        Gizmos.DrawWireCube(new Vector3(localPosition.x, maxY, localPosition.z), size);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(localPosition.x, minY + activationThreshold, localPosition.z), size);
    }
}
