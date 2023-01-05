using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonInteractable : XRBaseInteractable
{
    [Header("Custom Properties (Anything above is inherited)")]
    public UnityEvent OnPress = null;
    public UnityEvent OnRelease = null;

    [SerializeField, Tooltip("Assign a child GameObject button with a kinematic Rigidbody and Collider here. " +
        "Using this GameObject will work but might mess up the visualization.")] 
    private Transform buttonTransform;
    [SerializeField, Range(0f, 1f), Tooltip("The activation happens at this percentage height of the collider")] 
    private float activationThresholdPercent = 0.5f;

    [SerializeField, Tooltip("The minimum local height that the button may have. 0 is initial height")] 
    private float minY = -0.1f;
    [SerializeField, Tooltip("The maximum local height that the button may have. 0 is initial height.")] 
    private float maxY = 0f;

    private float activationThreshold;
    private bool wasPressedLastFrame = false;

    private IXRHoverInteractor hoverInteractor = null;
    private float previousHandHeight = 0f;


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
        activationThreshold = Mathf.Abs(minY - maxY) * activationThresholdPercent;
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
        ResetButton();
    }

    public void ResetButton()
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
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;

            float newPosition = buttonTransform.localPosition.y - handDifference;
            SetYPosition(newPosition);

            CheckPress();
        }
    }

    // Local y position allows any orientation of the button to work
    private float GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = buttonTransform.root.InverseTransformPoint(position);

        return localPosition.y;
    }

    // Just clamps the button height to the min/max
    private void SetYPosition(float position)
    {
        Vector3 newPosition = buttonTransform.localPosition;
        newPosition.y = Mathf.Clamp(position, minY, maxY);
        buttonTransform.localPosition = newPosition;
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
        else if (!isPressed && wasPressedLastFrame)
        {
            Debug.Log("Detected release");
            OnRelease.Invoke();
        }

        // This frame becomes last frame
        wasPressedLastFrame = isPressed;
    }

    private bool IsInPosition()
    {
        // Check if value remains the same after clamping to activation range; if true, it was in range
        float inRange = Mathf.Clamp(buttonTransform.localPosition.y, minY, minY + activationThreshold);

        return buttonTransform.localPosition.y == inRange;
    }

    #region Visualizer Gizmo
    // Show the min/max positions and the threshold in the editor as "planes"
    private void OnDrawGizmos()
    {
        Collider c = buttonTransform.GetComponent<Collider>();

        if (c != null && buttonTransform != null)
        {
            float debugActivationThreshold = Mathf.Abs(minY - maxY) * activationThresholdPercent;

            Vector3 localPosition = buttonTransform.localPosition;
            Vector3 size = c.bounds.size;
            size.y = 0.01f * size.y;

            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(
                new Vector3(localPosition.x, minY, localPosition.z), size);
            Gizmos.DrawWireCube(new Vector3(localPosition.x, maxY, localPosition.z), size);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(
                new Vector3(localPosition.x, minY + debugActivationThreshold, localPosition.z),
                size
                );

            Gizmos.color = Color.yellow;
            if (localPosition.y < (minY + debugActivationThreshold))
                Gizmos.color = Color.red;

            Gizmos.DrawWireCube(localPosition, size * 0.5f);
        }
    }
    #endregion
}
