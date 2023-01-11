using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] ButtonInteractable button;

    [SerializeField] Light intercomGreen;
    [SerializeField] Light intercomOrange;
    [SerializeField] Light controlpanelRed;

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
        deactivateIntercom();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            initializeIntercom();
            intercomGreen.intensity = 1.27f;
            intercomOrange.intensity = 1.27f;
            controlpanelRed.intensity = 0f;

        }

        else if (Input.GetKeyDown("a"))
        {
            deactivateIntercom();
            intercomGreen.intensity = 0f;
            intercomOrange.intensity = 0f;
            controlpanelRed.intensity = 1.27f;
        }

        // How to get a specific flag from the list of flags
        /*
        if (dialogue.flags["Family"]){
          Family on board
        }
        */

        if (dialogue.flags["Docking sound"] == true)
        {
            dockingSound.Play();
            dialogue.flags["Dialogue sound"] = false;
        }

        CheckCloseDialogue();
    }

    private void UpdateDialogue(int gameStateID)
    {
        
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
                    dialogue.flags[kvp.Key] = false;
                    break;
                }
            }
        }
    }

    //This function should be called every time the Intercom is initiated, I.e. at the start of every game state
    public void initializeIntercom()
    {

        // Activate dialogue window
        dialogueWindow.SetActive(true);

        // Deactivate console button
        button.enabled = false;

        // Set active dialogue
        UpdateDialogue(gameStateID);

        // Activate green and orange Intercom lights
        intercomGreen.intensity = 1.27f;
        intercomOrange.intensity = 1.27f;

        // Deactivate console light
        controlpanelRed.intensity = 0f;
    }

    // This function will be called every time a conversation has been completed. 
    public void deactivateIntercom()
    {
        // Disables dialogue window
        dialogueWindow.SetActive(false);

        // Deactivate console button
        button.enabled = true;

        //dialogue.startDialogue(0);
        intercomGreen.intensity = 0f;
        intercomOrange.intensity = 0f;

        // Activate console light
        controlpanelRed.intensity = 1.27f;
    }

}
