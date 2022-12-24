using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotatePlace : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] Transform placementPoint;
    [SerializeField] List<Button> buttons;
    [SerializeField, Range(-360, 360)] int distributionArc = 180;
    [SerializeField, Range(0, 360)] int startAngle = 0;
    [SerializeField] Transform visualElement;


    private void OnValidate()
    {
        PlaceElements(5);
    }

    void Start()
    {
        PlaceElements(5);
    }

    void Update()
    {
        
    }

    private void ResetRotation()
    {
        pivot.eulerAngles = new Vector3(pivot.eulerAngles.x, pivot.eulerAngles.y, startAngle);
    }

    public void PlaceElements(int number)
    {
        if (number < 1)
            return;

        ResetRotation();

        int increment = distributionArc / 5;
        int angle = startAngle;

        for (int i = 0; i < 5; i++)
        {
            buttons[i].transform.position = placementPoint.position;
            pivot.RotateAround(pivot.position, pivot.forward, -increment);
        }

        ResetRotation();
    }

    public void OnHoverElement(int index)
    {
        Vector3 direction = visualElement.position - buttons[index].transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        visualElement.Rotate(0f, 0f, angle);
    }

    private void OnDrawGizmosSelected()
    {
        // Display the distribution of all choices with current settings
        if (buttons == null || pivot == null)
            return;

        int increment = distributionArc / 5;
        int angle = startAngle;
        //Quaternion initial = pivot.rotation;

        for (int i = 0; i < 5; i++)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(placementPoint.position, 0.025f);
            pivot.RotateAround(pivot.position, pivot.forward, -increment);


            //buttons[i].transform.position = placementPoint.position;
            //Vector3.RotateTowards();
        }

        ResetRotation();
    }
}
