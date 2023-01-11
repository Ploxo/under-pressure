using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup myPanel;

    private bool fadeIn = false;
    private bool fadeOut = false;
    // Start is called before the first frame update

    public void ShowPanel()
    {
        fadeIn = true;
    }

    public void HidePanel()
    {
        fadeOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            if(myPanel.alpha < 1)
            {
                myPanel.alpha += Time.deltaTime;
                if(myPanel.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (myPanel.alpha > 0)
            {
                myPanel.alpha -= Time.deltaTime;
                if (myPanel.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }
}
