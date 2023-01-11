using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] ButtonInteractable button;

    [SerializeField] Light intercomGreen;
    [SerializeField] Light intercomOrange;
    [SerializeField] Light controlpanelRed;
    [SerializeField] LightButton mainButton;
    [SerializeField] Renderer intercomScreen;
    [SerializeField] Material baseScreen;

    [SerializeField] MeshRenderer tableScreen;
    [SerializeField] Material[] screens;
    [SerializeField] int screenIndex = 0;

    [SerializeField] PanelFade fader;

    [SerializeField] AudioSource ambience;
    [SerializeField] DialogueManager dialogue;
    [SerializeField] GameObject dialogueWindow;

    [SerializeField] AudioSource dockingSound;

    private int gameStateID = 0;

    // Start is called before the first frame update
    void Start()
    {
        //dialogue.flags[2];
        //initializeIntercom();

        SwapScreen(0);

        StartCoroutine(WaitForOneFrame());

        mainButton.SetButtonReady(false);
        // Delay the intercom for a bit
        Invoke("ReadyIntercom", 5f);
    }

    IEnumerator WaitForOneFrame(){
        yield return new WaitForEndOfFrame();

        deactivateIntercom();
        UnreadyIntercom();
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown("space"))
        //{
        //    initializeIntercom();
        //    intercomGreen.intensity = 1.27f;
        //    intercomOrange.intensity = 1.27f;
        //    controlpanelRed.intensity = 0f;

        //}

        //else if (Input.GetKeyDown("a"))
        //{
        //    deactivateIntercom();
        //    intercomGreen.intensity = 0f;
        //    intercomOrange.intensity = 0f;
        //    controlpanelRed.intensity = 1.27f;
        //}

        if (dialogue.flags["Docking sound"] == true)
        {
            dockingSound.Play();
            dialogue.flags["Dialogue sound"] = false;
        }

        CheckCloseDialogue();
    }

    private void CheckCloseDialogue()
    {
        foreach (KeyValuePair<string, bool> kvp in dialogue.flags)
        {
            if (kvp.Value)
            {
                if (kvp.Key.Contains(" end") && !kvp.Key.Contains("The"))
                {
                    deactivateIntercom();
                    UnreadyIntercom();
                    dialogue.flags[kvp.Key] = false;
                    break;
                }
            }
        }
    }

    public void PlayTransition()
    {
        // Play the transition here
        fader.ShowPanel();
        StopAllCoroutines();
        StartCoroutine(WaitForPanel(fader.durationFadeIn, 2f, fader.durationFadeOut));
    }

    IEnumerator WaitForPanel(float fadeInTime, float waitTime, float fadeOutTime)
    {
        while (fadeInTime > 0f)
        {
            fadeInTime -= Time.deltaTime;
            yield return null;
        }

        // Do stuff during fadeout
        while (waitTime > 0f)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        // Do stuff during wait
        SwapScreen((++screenIndex) % screens.Length);

        fader.HidePanel();

        while (fadeOutTime > 0f)
        {
            fadeOutTime -= Time.deltaTime;
            yield return null;
        }

        // Delay the intercom for a bit
        Invoke("ReadyIntercom", 5f);
        mainButton.SetButtonReady(false);
    }

    private void SwapScreen(int index)
    {

        var mats = tableScreen.materials;
        mats[1] = screens[index];
        tableScreen.materials = mats;
    }

    bool intercomReady = true;
    public void ReadyIntercom()
    {
        intercomReady = true;
        intercomOrange.intensity = 1.27f;
    }

    public void UnreadyIntercom()
    {
        intercomReady = false;
        intercomOrange.intensity = 0f;
    }


    public void ReadyMainButton()
    {
        mainButton.SetButtonReady(true);
    }

    public void UnReadyMainButton()
    {
        mainButton.SetButtonReady(false);
    }

    //This function should be called every time the Intercom is initiated, I.e. at the start of every game state
    public void initializeIntercom()
    {
        if (!intercomReady)
            return;

        // Activate dialogue window
        dialogueWindow.SetActive(true);

        // Deactivate console button
        button.enabled = false;

        // Activate green and orange Intercom lights
        intercomGreen.intensity = 1.27f;
        //intercomOrange.intensity = 1.27f;
    }

    // This function will be called every time a conversation has been completed. 
    public void deactivateIntercom()
    {
        intercomReady = false;

        // Disables dialogue window
        dialogueWindow.SetActive(false);

        var mats = intercomScreen.materials;
        mats[1] = baseScreen;
        intercomScreen.materials = mats;

        // Set control panel button ready
        mainButton.SetButtonReady(true);

        //dialogue.startDialogue(0);
        intercomGreen.intensity = 0f;
        intercomOrange.intensity = 0f;
    }

}
