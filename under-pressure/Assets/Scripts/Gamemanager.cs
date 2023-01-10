using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    [SerializeField] Light intercomGreen;
    [SerializeField] Light intercomOrange;
    [SerializeField] Light controlpanelRed;

    [SerializeField] AudioSource ambience;
    [SerializeField] DialogueManager dialogue;
    [SerializeField] GameObject dialogueWindow;


    private int gameStateID = 0;

    // Start is called before the first frame update
    void Start()
    {
        //dialogue.flags[2];
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            intializeIntercom();
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
         // Family on board
        }
        */
}

    //This function should be called every time the Intercom is initiated, I.e. at the start of every game state
    private void intializeIntercom()
    {

        // Deactivate console button
        dialogueWindow.SetActive(true);
        // Set active dialogue
        dialogue.startDialogue(gameStateID);

        // Prepare next gamestate
        if (gameStateID < 3)
        {
            gameStateID++;
        }
        // Activate green and orange Intercom lights
        intercomGreen.intensity = 1.27f;
        intercomOrange.intensity = 1.27f;

    }

    // This function will be called every time a conversation has been completed. 
    private void deactivateIntercom()
    {
        // Disables dialogue window
        dialogueWindow.SetActive(false);


        //dialogue.startDialogue(0);
        intercomGreen.intensity = 0f;
        intercomOrange.intensity = 0f;
    }

}
