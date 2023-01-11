using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup myPanel;
    [SerializeField] public float durationFadeOut = 5f;
    [SerializeField] public float durationFadeIn = 0.1f;

    private bool fadeIn = false;
    private bool fadeOut = false;

    private float timer = 0f;


    public void TurnOff()
    {
        myPanel.alpha = 0f;
    }

    public void ShowPanel()
    {
        timer = 0f;
        fadeIn = true;
    }

    public void HidePanel()
    {
        timer = durationFadeOut;
        fadeOut = true;
    }


    void Update()
    {
        if (fadeIn)
        {
            timer += Time.deltaTime;
            myPanel.alpha = Mathf.InverseLerp(0f, durationFadeIn, timer);

            if (myPanel.alpha >= 1f)
            {
                fadeIn = false;
            }
        }

        if (fadeOut)
        {
            timer -= Time.deltaTime;
            myPanel.alpha = Mathf.InverseLerp(0f, durationFadeOut, timer);

            if (myPanel.alpha == 0)
            {
                fadeOut = false;
            }
        }
    }
}
