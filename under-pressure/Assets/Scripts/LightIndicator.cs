using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIndicator : MonoBehaviour
{
    [SerializeField] private GameObject indicator;

    [SerializeField] private KeyCode key;

    private Light indicatorLight;
    private Material indicatorMaterial;

    private bool on;
    private float initialIntensity;
    private Color emissionColor;


    void Start()
    {
        indicatorMaterial = indicator.GetComponent<MeshRenderer>().material;
        emissionColor = indicatorMaterial.GetColor("_EmissionColor");

        indicatorLight = GetComponent<Light>();
        initialIntensity = indicatorLight.intensity;

        // Turn off at start
        ToggleIndicator();
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            ToggleIndicator();
        }
    }

    // Set the indicator active mode by turning on/off emission and its point light
    public void ToggleIndicator()
    {
        on = !on;

        if (on)
        {
            indicatorLight.intensity = 0f;
            indicatorMaterial.SetColor("_EmissionColor", Color.black);
        }
        else
        {
            indicatorLight.intensity = initialIntensity;
            indicatorMaterial.SetColor("_EmissionColor", emissionColor);
        }
    }
}
