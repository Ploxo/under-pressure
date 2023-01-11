using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogueManager : MonoBehaviour
{

    [SerializeField] public Dictionary<string, bool> flags;
    [SerializeField] public List<string> characters;
    [SerializeField] public List<DialogueUnit> dialogues;
    [SerializeField] private int currentDialogue = 0;


    [SerializeField] public List<GameObject> buttons;
    [SerializeField] public List<Material> imageMaterials;

    private int agentSound = 0;
    private int aristocratSound = 0;
    private int familySound = 0;
    private int overseerSound = 0;

    private List<string> endings;

    void Awake(){

        endings = new List<string>();

        flags = new Dictionary<string, bool>(){

            {"Overseer 1 end", false},
            {"Family yes", false},
            {"Family end", false},
            {"Family in cargo bay", false},
            {"Family in cleaning closet", false},
            {"Family in backup sleeping quarter", false},
            {"Aristocrat yes", false},
            {"Aristocrat end", false},
            {"Aristocrat in cargo bay", false},
            {"Aristocrat in cleaning closet", false},
            {"Aristocrat in backup sleeping quarter", false},
            {"Docking sound", false},
            {"Overseer 2 end", false},
            {"Nothing out of the ordinary", false},
            {"Tell on the family", false},
            {"Tell on the aristocrat", false},
            {"Tell on both", false},
            {"Agent end", false},
            {"Under command", false},
            {"Debt", false},
            {"The end", false},
            {"Family dialogue end", false},
            {"Aristocrat dialogue end", false},
            {"Ending 1", false},
            {"Ending 2", false},
            {"Ending 3", false},
            {"Ending 4", false},
            {"Ending 5", false},
            {"Ending 6", false},
            };

        endings.Add("The Captain gave the Agent a ride to his destination and payed him with some of the profits he received from selling the Ferrotassium. What happened to the Family or the Arristocrat we will never know. To be Continued...");
        endings.Add("The Captain gave the Agent a ride to his destination and payed him with some of the profits he received from selling the Ferrotassium. The Family managed to hide from the agent and sneak off the ship safely. What happened to the Aristocrat we will never know. To be Continued...");
        endings.Add("The Captain gave the Agent a ride to his destination and payed him with some of the profits he received from selling the Ferrotassium. The Aristocrat managed to hide from the agent and sneak off the ship safely. What happened to the Family we will never know. To be Continued...");
        endings.Add("The Family was taken into custody by the Agent and sent to a working camp. The Captain got off lightly with a small fine and a warning. What Happened to the Aristocrat we will never know. To be Continued...");
        endings.Add("The Aristocrat was taken into custody by the Agent and sent to a correctional facility. The Captain was given a fine and got demoted to work as the submarine's navigator. What happened to the Family we will never know. To be Continued...");
        endings.Add("The Family was taken into custody by the Agent and sent to a working camp. The Aristocrat managed to hide from the Agent and sneak off the ship safely. The Captain got off lightly with a small fine and a warning. To be Continued...");
        endings.Add("The Aristocrat was raken into custody by the Agent and sent to a correctional facility. The Family managed to hide from the Agent and sneak off the ship safely. The Captain was given a fine and got demoted to work as the submarine's navigator. To be Continued...");
        endings.Add("The Family was taken into custody by the Agent and sent to a working camp. The Aristocrat and Captain were both taken into custody by the Agent and sent to a correctional facility. To be Continued...");

    }

    // Start is called before the first frame update
    void Start()
    {
        
            //dialogues = new List<DialogueUnit>();

        for (int i = 0; i < buttons.Count; i++)
        {

            int tempI = i; // This does not work without this. If you replace 'tempI' with 'i' in the next line, 
                           //                                                  it always give the last possible iterator value. 
                           //                                                  Something about this whole () => Method() thing.

            buttons[i].GetComponent<Button>().onClick.AddListener(() => optionChosen(tempI));
        }


        startDialogue(0);
        //StartCoroutine(WaitForOneFrame());
    }

    IEnumerator WaitForOneFrame()
    {
        yield return new WaitForEndOfFrame();

        startDialogue(0);
    }

    // Update is called once per frame
    void Update()
    {
        checkFlags();
    }

    void checkFlags(){

        // Debug.Log("HALLO");
        if (currentDialogue == 18){
              Debug.Log("YES 18");

            if (flags["Family yes"] && !flags["Aristocrat yes"])
            {
                Debug.Log("YN");
                switchDialogue(19); 
            }
            if (!flags["Family yes"] && flags["Aristocrat yes"])
            {
                Debug.Log("NY");
                switchDialogue(20);
            }
            if (flags["Family yes"] && flags["Aristocrat yes"])
            {
                Debug.Log("YY");
                switchDialogue(21);
            }

            if (flags["The end"])
            {
                Debug.Log("end i guess");
                currentDialogue = 19;
                switchDialogue(19);
            }
        }

        if (flags["The end"] && flags["Ending 1"])
        {
            
        }
        else if (flags["The end"] && flags["Ending 2"])
        {

        }
        else if (flags["The end"] && flags["Ending 3"])
        {

        }
        else if (flags["The end"] && flags["Ending 4"])
        {

        }
        else if (flags["The end"] && flags["Ending 5"])
        {

        }
        else if (flags["The end"] && flags["Ending 6"])
        {

        }

    }

    public void startDialogue(int dialogueUnitId){

        DialogueUnit dialogueUnit = dialogues[dialogueUnitId];

        foreach (GameObject b in buttons){
            b.SetActive(true);
        }
        
        for (int i = buttons.Count - 1; i > dialogueUnit.choices.Count - 1; i--){
            buttons[i].SetActive(false);
        }

        for (int i = 0; i < dialogueUnit.choices.Count; i++){

            var textComponent = GameObject.Find(buttons[i].name + "/Text").GetComponent<TextMeshProUGUI>();
            textComponent.text = dialogueUnit.choices[i].text;

        }

        GameObject.Find("Dialogue/TextPanel/Text").GetComponent<TextMeshProUGUI>().text = dialogueUnit.text;

        Material charactersMaterial;

        switch (dialogueUnit.character)
        {
            case "Agent": 
                charactersMaterial = imageMaterials[0];
                playCharacterSound("Agent");
                break;

            case "Aristocrat": 
                charactersMaterial = imageMaterials[1];
                playCharacterSound("Aristocrat");
                break;

            case "Family": 
                charactersMaterial = imageMaterials[2];
                playCharacterSound("Family");
                break;

            case "Overseer": 
                charactersMaterial = imageMaterials[3];
                playCharacterSound("Overseer");
                break;

            default: 
                charactersMaterial = imageMaterials[4];
                break;
        
        }

        Renderer rend = GameObject.Find("Scenery/screen").GetComponent<Renderer>();

        var mats = rend.materials;
        mats[1] = charactersMaterial;
        rend.materials = mats;
    }

    void switchDialogue(int dialogueUnitId){
        currentDialogue = dialogueUnitId;
        startDialogue(currentDialogue);
    }


    private void playCharacterSound(string character)
    {
        switch (character)
        {
            case "Agent":
                if (agentSound == 0) {
                    GameObject.Find("Sounds/AgentSounds/agentSound1").GetComponent<AudioSource>().Play();                
                }
                if (agentSound == 1)
                {
                    GameObject.Find("Sounds/AgentSounds/agentSound2").GetComponent<AudioSource>().Play();
                }
                if (agentSound == 2)
                {
                    GameObject.Find("Sounds/AgentSounds/agentSound3").GetComponent<AudioSource>().Play();
                }
                if (agentSound == 3)
                {
                    GameObject.Find("Sounds/AgentSounds/agentSound4").GetComponent<AudioSource>().Play();
                }
                agentSound = (agentSound + 1) % 4;
                break;

            case "Aristocrat":
                if (aristocratSound == 0)
                {
                    GameObject.Find("Sounds/AristocratSounds/aristocratSound1").GetComponent<AudioSource>().Play();
                }
                if (aristocratSound == 1)
                {
                    GameObject.Find("Sounds/AristocratSounds/aristocratSound2").GetComponent<AudioSource>().Play();
                }
                if (aristocratSound == 2)
                {
                    GameObject.Find("Sounds/AristocratSounds/aristocratSound3").GetComponent<AudioSource>().Play();
                }
                if (aristocratSound == 3)
                {
                    GameObject.Find("Sounds/AristocratSounds/aristocratSound4").GetComponent<AudioSource>().Play();
                }
                aristocratSound = (aristocratSound + 1) % 4;
                break;

            case "Family":
                if (familySound == 0)
                {
                    GameObject.Find("Sounds/FamilySounds/familySound1").GetComponent<AudioSource>().Play();
                }
                if (familySound == 1)
                {
                    GameObject.Find("Sounds/FamilySounds/familySound2").GetComponent<AudioSource>().Play();
                }
                if (familySound == 2)
                {
                    GameObject.Find("Sounds/FamilySounds/familySound3").GetComponent<AudioSource>().Play();
                }
                familySound = (familySound + 1) % 3;
                break;

            case "Overseer":
                if (overseerSound == 0)
                {
                    GameObject.Find("Sounds/OverseerSounds/overseerSound1").GetComponent<AudioSource>().Play();
                }
                if (overseerSound == 1)
                {
                    GameObject.Find("Sounds/OverseerSounds/overseerSound2").GetComponent<AudioSource>().Play();
                }
                if (overseerSound == 2)
                {
                    GameObject.Find("Sounds/OverseerSounds/overseerSound3").GetComponent<AudioSource>().Play();
                }
                overseerSound = (overseerSound + 1) % 3;
                break;

            default:
                break;
        }
    }

    private void optionChosen(int buttonIndex){

        ChoiceUnit choice = dialogues[currentDialogue].choices[buttonIndex];

        // Debug.Log("Option chosen: " + choice.text);

        foreach (string flag in choice.flags){
            // Debug.Log("Setting flag: " + flag);
            // Debug.Log("Flag was: " + this.flags[flag]);
            this.flags[flag] = true;
            // Debug.Log("Flag set to: " + this.flags[flag]);

        }

        currentDialogue = choice.nextDialogueUnit;
        startDialogue(currentDialogue);
    }


    // TODO: Fix the ID duplications (use the array enumeration instead, I guess?)
    private void addDialogue(){

            // ID = 0
            // Adding a new dialogue line
            dialogues.Add(

                // This is a DialogueUnit object, it holds the line of text to be displayed, and the possible replies
                new DialogueUnit(

                    "Hey there! Do you like bananas or apples?", // The text line to be displayed
                    "The Boss", // Name of the character to be displayed as a part of this line
                    // Now we create a list of all possible replies
                    new List<ChoiceUnit>(){

                        // Each reply option is an OptionUnit object, holding the line of text to appear in the reply button, id of the dialogue unit that this option leads to, 
                        //                                              and a list of flags that need to be set to true when this option is chosen 
                        new ChoiceUnit(
                            "Apples!", // The text of this reply option
                            1, // The id of the next dialogue unit if this line is chosen
                            new List<string>(){"likes_apples"} // Flags for the Dialogue Manager to set if this line is chosen

                        ),
                        // Second option
                        new ChoiceUnit(
                            "Bananas!",
                            2,
                            new List<string>(){} // Empty list to show that no flags need to be set
                        )

                    }
                )

            );

            // ID = 1
            dialogues.Add(

                new DialogueUnit(
                    "Me too! I love apples!",
                    "The Boss",
                    new List<ChoiceUnit>(){
                        new ChoiceUnit(
                            "I'll get you some apples, then!",
                            3,
                            new List<string>(){}
                        )
                    }
                )
            );

            // ID = 2
            dialogues.Add(

                new DialogueUnit(
                    "Bananas, really?",
                    "The Boss",
                    new List<ChoiceUnit>(){
                        new ChoiceUnit(
                            "I guess I'll get you some apples, then!",
                            3,
                            new List<string>(){}
                        )
                    }
                )
            );

            // ID = 3
            dialogues.Add(

                new DialogueUnit(
                    "Exactly, get me some god-damn apples!",
                    "The Boss",
                    new List<ChoiceUnit>(){
                        new ChoiceUnit(
                            "This dialogue option is the last, and leads back to itself",
                            3,
                            new List<string>(){}
                        )
                    }
                )
            );

    }
}
