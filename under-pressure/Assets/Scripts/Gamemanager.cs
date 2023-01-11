using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] ButtonInteractable button;

    [SerializeField] Light intercomGreen;
    [SerializeField] Light intercomOrange;
    [SerializeField] Light controlpanelRed;
    [SerializeField] LightButton mainButton;
    [SerializeField] Renderer intercomScreen;
    [SerializeField] Material baseScreen;

    [SerializeField] GameObject submarine;

    [SerializeField] MeshRenderer tableScreen;
    [SerializeField] Material[] screens;
    [SerializeField] int screenIndex = 0;

    [SerializeField] PanelFade fader;
    [SerializeField] PanelFade endFader;

    [SerializeField] TMP_Text endText;

    [SerializeField] AudioSource ambience;
    [SerializeField] DialogueManager dialogue;
    [SerializeField] GameObject dialogueWindow;

    [SerializeField] AudioSource dockingSound;
    [SerializeField] AudioSource beepSound;
    [SerializeField] AudioSource engineSound;

    private List<string> endings;

    private bool initialStart = true;

    private int gameStateID = 0;

    void Awake()
    {
        endings = new List<string>();

        endings.Add("The Captain gave the Agent a ride to his destination and payed him with some of the profits he received from selling the Ferrotassium. What happened to the Family or the Aristocrat we will never know. To Be Continued...");
        endings.Add("The Captain gave the Agent a ride to his destination and payed him with some of the profits he received from selling the Ferrotassium. The Family managed to hide from the agent and sneak off the ship safely. What happened to the Aristocrat we will never know. To Be Continued...");
        endings.Add("The Captain gave the Agent a ride to his destination and payed him with some of the profits he received from selling the Ferrotassium. The Aristocrat managed to hide from the agent and sneak off the ship safely. What happened to the Family we will never know. To Be Continued...");
        endings.Add("The Family was taken into custody by the Agent and sent to a working camp. The Captain got off lightly with a small fine and a warning. What Happened to the Aristocrat we will never know. To Be Continued...");
        endings.Add("The Aristocrat was taken into custody by the Agent and sent to a correctional facility. The Captain was given a fine and got demoted to work as the submarine's navigator. What happened to the Family we will never know. To Be Continued...");
        endings.Add("The Family was taken into custody by the Agent and sent to a working camp. The Aristocrat managed to hide from the Agent and sneak off the ship safely. The Captain got off lightly with a small fine and a warning. To Be Continued...");
        endings.Add("The Aristocrat was raken into custody by the Agent and sent to a correctional facility. The Family managed to hide from the Agent and sneak off the ship safely. The Captain was given a fine and got demoted to work as the submarine's navigator. To Be Continued...");
        endings.Add("The Family was taken into custody by the Agent and sent to a working camp. The Aristocrat and Captain were both taken into custody by the Agent and sent to a correctional facility. To Be Continued...");
    }

    // Start is called before the first frame update
    void Start()
    {
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
        if (dialogue.flags["Docking sound"] == true)
        {
            dockingSound.Play();
            dialogue.flags["Docking sound"] = false;
        }

        if (dialogue.flags["The end"] == true){
            playEndTransition();
            // var myEnumerator = dialogue.flags.GetEnumerator();
            // while(myEnumerator.MoveNext()){
            //     dialogue.flags[myEnumerator.Current.Key] = false;
            // }
            dialogue.flags["The end"] = false;
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
                    if (kvp.Key == "Family end" || kvp.Key == "Aristocrat end" || kvp.Key == "Agent end")
                    {
                        dialogue.flags["Family end"] = false;
                        dialogue.flags["Aristocrat end"] = false;
                        dialogue.flags["Agent end"] = false;

                        ActivateTravel();
                        break;
                    }
                    else
                    {
                        dialogue.flags[kvp.Key] = false;

                        deactivateIntercom();
                        UnreadyIntercom();

                        // Delay the intercom for a bit
                        Invoke("ReadyIntercom", Random.Range(5, 7));
                        break;
                    }
                }
            }
        }
    }

    public void PlayTransition()
    {
        // Play the transition here
        engineSound.Play();
        fader.ShowPanel();
        StopAllCoroutines();
        StartCoroutine(WaitForPanel(fader.durationFadeIn, 2f, fader.durationFadeOut));
    }

    public void playEndTransition()
    {
        endFader.ShowPanel();
        setEndText();
        // submarine.SetActive(false);
        StopAllCoroutines();
    }

    public void setEndText()
    {
        if(dialogue.flags["Family yes"] && dialogue.flags["Aristocrat yes"])
        {
            if(dialogue.flags["Nothing out of the ordinary"])
            {
                endText.text = endings[7];
            }
            else if(dialogue.flags["Tell on the family"])
            {
                if (sameRoom())
                {
                    endText.text = endings[7];
                }
                else
                {
                    endText.text = endings[5];
                }
            }
            else if(dialogue.flags["Tell on the aristocrat"])
            {
                if (sameRoom())
                {
                    endText.text = endings[7];
                }
                else
                {
                    endText.text = endings[6];
                }
            }
            else
            {
                endText.text = endings[7];
            }
        }
        else if(dialogue.flags["Family yes"])
        {
            if (dialogue.flags["Nothing out of the ordinary"])
            {
                endText.text = endings[1];
            }
            else if(dialogue.flags["Tell on the family"])
            {
                endText.text = endings[3];
            }
        }
        else if(dialogue.flags["Aristocrat yes"])
        {
            if (dialogue.flags["Nothing out of the ordinary"])
            {
                endText.text = endings[2];
            }
            else if (dialogue.flags["Tell on the aristocrat"])
            {
                endText.text = endings[4];
            }
        }
        else
        {
            if (dialogue.flags["Nothing out of the ordinary"])
            {
                endText.text = endings[0];
            }
        }
        
    }

    public bool sameRoom()
    {
        if(dialogue.flags["Family in cargo bay"] && dialogue.flags["Aristocrat in cargo bay"])
        {
            return true;
        }
        if(dialogue.flags["Family in cleaning closet"] && dialogue.flags["Aristocrat in cleaning closet"])
        {
            return true;
        }
        if(dialogue.flags["Family in backup sleeping quarter"] && dialogue.flags["Aristocrat in backup sleeping quarter"])
        {
            return true;
        }
        return false;
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

        //mainButton.SetButtonReady(false);
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
        beepSound.Play();
    }

    public void UnreadyIntercom()
    {
        intercomReady = false;
        intercomOrange.intensity = 0f;
    }


    public void ReadyMainButton()
    {
        Debug.Log("Hallo I am ready");
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

        //// Deactivate console button
        //button.enabled = false;

        // Activate green and orange Intercom lights
        intercomGreen.intensity = 1.27f;
    }

    // This function will be called every time a conversation has been completed. 
    public void deactivateIntercom()
    {
        intercomReady = false;


        var mats = intercomScreen.materials;
        mats[1] = baseScreen;
        intercomScreen.materials = mats;

        // Disables dialogue window
        dialogueWindow.SetActive(false);

        //dialogue.startDialogue(0);
        intercomGreen.intensity = 0f;
        intercomOrange.intensity = 0f;
    }

    public void ActivateTravel()
    {
        deactivateIntercom();

        initialStart = false;

        // Set control panel button ready
        if (!initialStart)
        {
            mainButton.SetButtonReady(true);
        }
    }

}
