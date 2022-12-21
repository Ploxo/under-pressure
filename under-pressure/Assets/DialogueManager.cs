using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
[System.Serializable]
public class DialogueManager : MonoBehaviour
{

    [SerializeField] public Dictionary<string, bool> flags;
    [SerializeField] public List<string> characters;
    [SerializeField] public List<DialogueUnit> dialogues;
    [SerializeField] private int currentDialogue = 0;


    [SerializeField] public List<GameObject> buttons;


    // Start is called before the first frame update
    void Start()
    {
        
            dialogues = new List<DialogueUnit>();

            flags = new Dictionary<string, bool>(){

                {"likes_apples", false},

            };

            for (int i = 0; i < buttons.Count; i++){

                int tempI = i; // This does not work without this. If you replace 'tempI' with 'i' in the next line, 
                //                                                  it always give the last possible iterator value. 
                //                                                  Something about this whole () => Method() thing.

                buttons[i].GetComponent<Button>().onClick.AddListener(() => optionChosen(tempI));
            }

            addDialogue();

            startDialogue(currentDialogue);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void startDialogue(int dialogueUnitId){

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

        GameObject.Find("Dialogue/Text").GetComponent<TextMeshProUGUI>().text = dialogueUnit.text;

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

                // This is a DialogueUnit object the line of text to be displayed, and the possible replies
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