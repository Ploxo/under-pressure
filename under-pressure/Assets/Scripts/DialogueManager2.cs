using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

[ExecuteInEditMode]
[System.Serializable]
public class DialogueManager2 : MonoBehaviour
{

    [SerializeField] public Dictionary<string, bool> flags;
    [SerializeField] public List<string> characters;
    [SerializeField] public List<DialogueUnit> dialogues;
    [SerializeField] private int currentDialogue = 0;


    [SerializeField] public List<GameObject> buttons;

    private string tempString = "";
    private string tempString2 = "";
    private string tempString3 = "";

    // Start is called before the first frame update
    void Start()
    {

        dialogues = new List<DialogueUnit>();

        flags = new Dictionary<string, bool>(){
                {"Family", false},
                {"Aristocrat", false},
                {"Family in cargo bay", false},
                {"Family in cleaning closet", false},
                {"Family in backup sleeping quarter", false},
                {"Aristocrat in cargo bay", false},
                {"Aristocrat in cleaning closet", false},
                {"Aristocrat in backup sleeping quarter", false},
                {"Same place", false}
            };

        for (int i = 0; i < buttons.Count; i++)
        {

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

    private void startDialogue(int dialogueUnitId)
    {

        DialogueUnit dialogueUnit = dialogues[dialogueUnitId];

        foreach (GameObject b in buttons)
        {
            b.SetActive(true);
        }

        for (int i = buttons.Count - 1; i > dialogueUnit.choices.Count - 1; i--)
        {
            buttons[i].SetActive(false);
        }

        for (int i = 0; i < dialogueUnit.choices.Count; i++)
        {

            var textComponent = GameObject.Find(buttons[i].name + "/Text").GetComponent<TextMeshProUGUI>();
            textComponent.text = dialogueUnit.choices[i].text;

        }

        GameObject.Find("Dialogue/TextPanel/Text").GetComponent<TextMeshProUGUI>().text = dialogueUnit.text;

    }

    private void optionChosen(int buttonIndex)
    {

        ChoiceUnit choice = dialogues[currentDialogue].choices[buttonIndex];

        // Debug.Log("Option chosen: " + choice.text);

        foreach (string flag in choice.flags)
        {
            // Debug.Log("Setting flag: " + flag);
            // Debug.Log("Flag was: " + this.flags[flag]);
            this.flags[flag] = true;
            // Debug.Log("Flag set to: " + this.flags[flag]);

        }

        currentDialogue = choice.nextDialogueUnit;
        startDialogue(currentDialogue);



    }


    // TODO: Fix the ID duplications (use the array enumeration instead, I guess?)
    private void addDialogue()
    {

        dialogues.Add(
            // dialogue 0
            new DialogueUnit(

                "Greetings, transport module 589, your shipment has been received at the port.", // The text line to be displayed
                "The Boss", // Name of the character to be displayed as a part of this line
                            // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {
                        new ChoiceUnit(
                            "Continue",
                            1,
                            new List<string>(){}
                            )
                }

                )
            );

        dialogues.Add(
            // dialogue 1
            new DialogueUnit(

                "Your next instruction is to deliver 120 crates of Ferrotassium to the Galthix  republic at the Barren Oceans", // The text line to be displayed
                "The Boss", // Name of the character to be displayed as a part of this line
                            // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                        new ChoiceUnit(
                            "Continue",
                            2,
                            new List<string>(){}
                            )
                }

                )
            );

        dialogues.Add(
            // dialogue 2
            new DialogueUnit(

                "Keep on your guard, our shipments do not usually cruise here, make sure to not bring any unwanted crew on-board, understood?", // The text line to be displayed
                "The Boss", // Name of the character to be displayed as a part of this line
                            // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                        new ChoiceUnit(
                            "Continue",
                            3,
                            new List<string>(){}
                            )
                }

                )
            );

        dialogues.Add(
            // dialogue 3
            new DialogueUnit(

                "A detailed view of your path has been uploaded to your navigation table", // The text line to be displayed
                "The Boss", // Name of the character to be displayed as a part of this line
                            // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                        new ChoiceUnit(
                            "Continue",
                            4,
                            new List<string>(){}
                            )
                }

                )
            );

        dialogues.Add(
            // dialogue 4
            new DialogueUnit(

                "Good luck, transport module 589, over and out.", // The text line to be displayed
                "The Boss", // Name of the character to be displayed as a part of this line
                            // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                        "Continue",
                        5,
                        new List<string>(){}
                        )
                }

                )
            );

        dialogues.Add(
            // dialogue 5
            new DialogueUnit(

                "Please kind sir, let us board your vessel! Our child is starving and we have no other way to our destination!", // The text line to be displayed
                "Family", // Name of the character to be displayed as a part of this line
                          // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                        "Continue",
                        6,
                        new List<string>(){}
                        )
                }
                )
            );

        dialogues.Add(

            // dialogue 6
            new DialogueUnit(

                "Please, rescue us, you know how the new government treats people like us!", // The text line to be displayed
                "Family", // Name of the character to be displayed as a part of this line
                          // Now we create a list of all possible replies
                new List<ChoiceUnit>(){

                    new ChoiceUnit(
                        "No, I cannot risk it.",
                        11,
                        new List<string>(){"Family in cargo bay"}
                        ),

                    new ChoiceUnit(
                        "Yes, you can stay in the cleaning closet.",
                        7,
                        new List<string>(){"Family in cleaning closet"}
                        ),

                    new ChoiceUnit(
                        "Yes, you can stay in the backup room.",
                        7,
                        new List<string>(){"Family in backup sleeping quarter"}
                        ),

                    new ChoiceUnit(
                        "Yes, you can stay in the cargo bay.",
                        7,
                        new List<string>(){"Family in cargo bay"}
                        )

                }
                )
            );

        dialogues.Add(
            // dialogue 7

            new DialogueUnit(

                "We appreciate your kindness to let us stay! Is there anything you are wondering about?", // The text line to be displayed
                "Family", // Name of the character to be displayed as a part of this line
                          // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                        "Why are you travelling?",
                        8,
                        new List<string>(){}
                        ),

                    new ChoiceUnit(
                       "Why do you need a ride?",
                        9,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Is it comfortable here?",
                        10,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "No",
                        11,
                        new List<string>(){}
                        )
                }
                )
            );

        dialogues.Add(
            // dialogue 8

            new DialogueUnit(

                "“Our son and mother have a sickness that our doctors refuse to cure for us due to our race. He told us we need to go get a certification from the manufacturers first.", // The text line to be displayed
                "Family", // Name of the character to be displayed as a part of this line
                          // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                       "Why do you need a ride?",
                        9,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Is it comfortable here?",
                        10,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Continue",
                        11,
                        new List<string>(){}
                        )
                }
                )
            );

        dialogues.Add(
            // dialogue 9

            new DialogueUnit(

                "So much of our money has been drained to keep our sick alive and fed. We don’t have money for fancy vehicles…", // The text line to be displayed
                "Family", // Name of the character to be displayed as a part of this line
                          // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                        "Why are you travelling?",
                        8,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Is it comfortable here?",
                        10,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Continue",
                        11,
                        new List<string>(){}
                        )
                }
                )
            );

        if (flags["Family in cargo bay"] == true)
        {
            tempString = "I suppose… Could be worse…";
        }
        else if (flags["Family in cleaning closet"] == true)
        {
            tempString = "It is cramped, but we are grateful for the space.";
        }
        else if (flags["Family in backup sleeping quarter"] == true)
        {
            tempString = "It is cramped, but we are grateful for the space.";
        }

        dialogues.Add(
            // dialogue 10

            new DialogueUnit(
                tempString, // The text line to be displayed
                "Family", // Name of the character to be displayed as a part of this line
                          // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                        "Why are you travelling?",
                        8,
                        new List<string>(){}
                        ),

                    new ChoiceUnit(
                       "Why do you need a ride?",
                        9,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Is it comfortable here?",
                        10,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Continue",
                        11,
                        new List<string>(){}
                        )
                }
                )
            );

        dialogues.Add(
            // dialogue 11 // Aristocrat arrives

            new DialogueUnit(

                "I do not have time for dabble, I require a transport to the Kothos Republic at once! I have important meetings to attend to.", // The text line to be displayed
                "Aristocrat", // Name of the character to be displayed as a part of this line
                              // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                        "Continue",
                        12,
                        new List<string>(){}
                        )
                }
                )
            );

        dialogues.Add(
            // dialogue 12

            new DialogueUnit(

                "I will pay you 280 thousand Kropos if you take me there.", // The text line to be displayed
                "Aristocrat", // Name of the character to be displayed as a part of this line
                              // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                        "Continue",
                        13,
                        new List<string>(){}
                        )
                }
                )
            );

        dialogues.Add(
            // dialogue 13

            new DialogueUnit(

                "Well?", // The text line to be displayed
                "Aristocrat", // Name of the character to be displayed as a part of this line
                              // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                        "How can I refuse? Welcome on board.",
                        15,
                        new List<string>(){}
                        ),

                    new ChoiceUnit(
                        "I am sorry, but I cannot take you there.",
                        14,
                        new List<string>(){}
                        )
                }
                )
            );

        dialogues.Add(
            // dialogue 14

            new DialogueUnit(

                "You will regret this…!", // The text line to be displayed
                "Aristocrat", // Name of the character to be displayed as a part of this line
                              // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                        "Continue",
                        20, // change
                        new List<string>(){}
                        )
                }
                )
            );

        dialogues.Add(
            // dialogue 15

            new DialogueUnit(

                "Naturally. Inform me of your most suited quarters for residence.", // The text line to be displayed
                "Aristocrat", // Name of the character to be displayed as a part of this line
                              // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                        "Yes, you can stay in the cargo bay.",
                        16,
                        new List<string>(){"Aristocrat in cargo bay"}
                        ),
                    new ChoiceUnit(
                        "Yes, you can stay in the cleaning closet.",
                        16,
                        new List<string>(){"Aristocrat in cleaning closet"}
                        ),

                    new ChoiceUnit(
                        "Yes, you can stay in the backup room.",
                        16,
                        new List<string>(){"Aristocrat in backup sleeping quarter"}
                        )
                }
                )
            );

        if (flags["Family in cargo bay"] == true && flags["Aristocrat in cargo bay"] == true || flags["Family in cleaning closet"] == true && flags["Aristocrat in cleaning closet"] == true || flags["Family in backup sleeping quarter"] == true && flags["Aristocrat in backup sleeping quarter"] == true)
        {
            flags["Same place"] = true;
        }

        /*
        if (flags["Family in cargo bay"] == true)
        {
            dialogues.Add(
                // dialogue 15

                new DialogueUnit(

                    "Naturally. Inform me of your most suited quarters for residence.", // The text line to be displayed
                    "Aristocrat", // Name of the character to be displayed as a part of this line
                              // Now we create a list of all possible replies
                    new List<ChoiceUnit>(){
                       new ChoiceUnit(
                        "Yes, you can stay in the cleaning closet.",
                        16,
                        new List<string>(){ "Aristocrat in cleaning closet" }
                        ),

                    new ChoiceUnit(
                        "Yes, you can stay in the backup room.",
                        16,
                        new List<string>(){ "Aristocrat in backup sleeping quarter" }
                        ),

                    }
                    )
                );
        }
        else if (flags["Family in cleaning closet"] == true)
        {
            dialogues.Add(
                // dialogue 15

                new DialogueUnit(

                    "Naturally. Inform me of your most suited quarters for residence.", // The text line to be displayed
                    "Aristocrat", // Name of the character to be displayed as a part of this line
                              // Now we create a list of all possible replies
                    new List<ChoiceUnit>(){

                    new ChoiceUnit(
                        "Yes, you can stay in the backup room.",
                        16,
                        new List<string>(){ "Aristocrat in backup sleeping quarter" }
                        ),

                    new ChoiceUnit(
                        "Yes, you can stay in the cargo bay.",
                        16,
                        new List<string>(){ "Aristocrat in cargo bay" }
                        )
                    }
                    )
                );
        }
        else if (flags["Family in backup sleeping quarter"] == true)
        {
            dialogues.Add(
                // dialogue 15

                new DialogueUnit(

                    "Naturally. Inform me of your most suited quarters for residence.", // The text line to be displayed
                    "Aristocrat", // Name of the character to be displayed as a part of this line
                              // Now we create a list of all possible replies
                    new List<ChoiceUnit>(){
                        new ChoiceUnit(
                        "Yes, you can stay in the cleaning closet.",
                        16,
                        new List<string>(){ "Aristocrat in cleaning closet" }
                        ),

                    new ChoiceUnit(
                        "Yes, you can stay in the cargo bay.",
                        16,
                        new List<string>(){ "Aristocrat in cargo bay" }
                        )
                    }
                    )
                );
        }*/

        dialogues.Add(
            // dialogue 16

            new DialogueUnit(

                "What do you want?", // The text line to be displayed
                "Aristocrat", // Name of the character to be displayed as a part of this line
                              // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                        "Why are you travelling?",
                        17,
                        new List<string>(){}
                        ),

                    new ChoiceUnit(
                       "Why do you need a ride?",
                        18,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Is it comfortable here?",
                        19,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Continue",
                        20,
                        new List<string>(){}
                        )
                }
                )
            );

        dialogues.Add(
            // dialogue 17

            new DialogueUnit(

                "I am travelling to the estate of Sir Blurts, you do not need to know the details of my affairs", // The text line to be displayed
                "Aristocrat", // Name of the character to be displayed as a part of this line
                              // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                       "Why do you need a ride?",
                        18,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Is it comfortable here?",
                        19,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Continue",
                        20,
                        new List<string>(){}
                        )
                }
                )
            );

        dialogues.Add(
            // dialogue 18

            new DialogueUnit(

                "Being discreet in my line of business has its advantages, as you should know by how much I’ve paid you.", // The text line to be displayed
                "Aristocrat", // Name of the character to be displayed as a part of this line
                              // Now we create a list of all possible replies
                new List<ChoiceUnit>(){
                    new ChoiceUnit(
                        "Why are you travelling?",
                        17,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Is it comfortable here?",
                        19,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Continue",
                        20,
                        new List<string>(){}
                        )
                }
                )
            );

        // depends on where the aristocrat is

        if (flags["Aristocrat in cargo bay"] == true)
        {
            tempString2 = "Your supply of Ferrotasium seems to be quite decent, would you like to part with a few crates? It is the least you could do after putting me in this filth.";
        }
        else if (flags["Aristocrat in cleaning closet"] == true)
        {
            tempString2 = "I will remember this treatment ‘Captain’, you have truly surprised me.";
        }
        else if (flags["Aristocrat in backup sleeping quarter"] == true)
        {
            tempString2 = "I am not used to such a cramped squallid place, but I suppose it shall do.";
        }
        if (flags["Same place"] == true)
        {
            tempString2 = "Now I know why this place stinks, you keep rabble in your ship!";
        }

        /*if (flags["Family in cargo bay"] == true && flags["Aristocrat in cargo bay"] == true || flags["Family in cleaning closet"] == true && flags["Aristocrat in cleaning closet"] == true || flags["Family in backup sleeping quarter"] == true && flags["Aristocrat in backup sleeping quarter"] == true)
    {
        dialogues.Add(
        // dialogue 19

        new DialogueUnit(

            "Now I know why this place stinks, you keep rabble in your ship!", // The text line to be displayed
            "Aristocrat", // Name of the character to be displayed as a part of this line
                          // Now we create a list of all possible replies
            new List<ChoiceUnit>(){
                new ChoiceUnit(
                    "Why are you travelling?",
                    17,
                    new List<string>(){}
                    ),
                new ChoiceUnit(
                    "Why do you need a ride?",
                    18,
                    new List<string>(){}
                    ),
                new ChoiceUnit(
                    "Continue",
                    20,
                    new List<string>(){}
                    )
            }
            )
        );
    }*/
        dialogues.Add(
        // dialogue 19

        new DialogueUnit(

            tempString2, // The text line to be displayed
            "Aristocrat", // Name of the character to be displayed as a part of this line
                          // Now we create a list of all possible replies
            new List<ChoiceUnit>(){
                    new ChoiceUnit(
                        "Why are you travelling?",
                        17,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Why do you need a ride?",
                        18,
                        new List<string>(){}
                        ),
                    new ChoiceUnit(
                        "Continue",
                        20,
                        new List<string>(){}
                        )
            }
            )
        );

        dialogues.Add(
                  // dialogue 20
                  new DialogueUnit(

                      "Greetings, transport module 589, you may approach the dock.", // The text line to be displayed
                      "The Overseer", // Name of the character to be displayed as a part of this line
                                      // Now we create a list of all possible replies
                      new List<ChoiceUnit>()
                      {
                        new ChoiceUnit(
                            "Continue",
                            21,
                            new List<string>(){}
                            )
                      }

                      )
                  );

        dialogues.Add(
            // dialogue 21
            new DialogueUnit(

                "Very good, Captain. I will send a team to assist with the cargo transfer and any other needs you may have.", // The text line to be displayed
                "The Overseer", // Name of the character to be displayed as a part of this line
                                // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {
                        new ChoiceUnit(
                            "Continue",
                            22,
                            new List<string>(){}
                            )
                }

                )
            );

        dialogues.Add(
            // dialogue 22
            new DialogueUnit(

                "Safe travels Captain.", // The text line to be displayed
                "The Overseer", // Name of the character to be displayed as a part of this line
                                // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {
                        new ChoiceUnit
                        (
                            "Continue",
                            23,
                            new List<string>(){}
                        )
                }

                            )
                    );

        dialogues.Add(
            // dialogue 23
            new DialogueUnit(

                "Captain, I am an agent of the Kothos Republic, my badge number is K9G7D54, under the decree of our Supreme Leader Urilius I demand passage to Belium Station.", // The text line to be displayed
                "Agent", // Name of the character to be displayed as a part of this line
                             // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                                {
                                 new ChoiceUnit
                                    (
                                            "Continue",
                                             24,
                                             new List<string>(){}
                                         )
                                }

                            )
                    );

        dialogues.Add(
            // dialogue 24
            new DialogueUnit(

                "I'm sorry, but we are only authorised to transport cargo and crew. Transporting passengers is strictly prohibited.", // The text line to be displayed
                "Captain", // Name of the character to be displayed as a part of this line
                           // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {
                        new ChoiceUnit(
                            "Continue",
                            25,
                            new List<string>(){}
                            )
                }

                )
            );

        dialogues.Add(
            // dialogue 25
            new DialogueUnit(

                "Your vessel is the only one going in that direction in the next few days, this requisitioning has already been approved. This is not a request", // The text line to be displayed
                "Agent", // Name of the character to be displayed as a part of this line
                         // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {
                        new ChoiceUnit(
                            "Continue",
                            26,
                            new List<string>(){}
                            )
                }

                )
            );

        dialogues.Add(
            // dialogue 26
            new DialogueUnit(

                "I see. In that case, come aboard, I do have some space, just take care as the cargo can be temperamental at our depths. ", // The text line to be displayed
                "Captain", // Name of the character to be displayed as a part of this line
                           // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {
                        new ChoiceUnit(
                            "Continue",
                            27,
                            new List<string>(){}
                            )
                }

                )
            );

        if (flags["Family"] == true && flags["Aristocrat"] == false)
        {
            dialogues.Add(
            // dialogue 27
            new DialogueUnit(

                "Captain, may I have a word?", // The text line to be displayed
                "Agent", // Name of the character to be displayed as a part of this line
                             // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {
                        new ChoiceUnit(
                            "Continue",
                            32,
                            new List<string>(){}
                            ),

                        new ChoiceUnit(
                            "Expose family",
                            28,
                            new List<string>(){}
                            )
                }

                )
            );
        }
        else if (flags["Family"] == false && flags["Aristocrat"] == true)
        {
            dialogues.Add(
            // dialogue 27
            new DialogueUnit(

                "Captain, may I have a word?", // The text line to be displayed
                "Agent", // Name of the character to be displayed as a part of this line
                             // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {
                        new ChoiceUnit(
                            "Continue",
                            32,
                            new List<string>(){}
                            ),

                        new ChoiceUnit(
                            "Expose aristocrat",
                            33,
                            new List<string>(){}
                            )
                }

                )
            );
        }
        else if (flags["Family"] == true && flags["Aristocrat"] == true && flags["Same place"] == false)
        {
            dialogues.Add(
            // dialogue 27
            new DialogueUnit(

                "Captain, may I have a word?", // The text line to be displayed
                "Agent", // Name of the character to be displayed as a part of this line
                         // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {

                        new ChoiceUnit(
                            "Expose family",
                            45,
                            new List<string>(){}
                            ),

                        new ChoiceUnit(
                            "Expose aristocrat",
                            39,
                            new List<string>(){}
                            ),

                        new ChoiceUnit(
                            "Expose both",
                            51,
                            new List<string>(){}
                            )
                }

                )
            );
        }
        else if (flags["Same place"] == true)
        {
            dialogues.Add(
            // dialogue 27
            new DialogueUnit(

                "Captain, may I have a word?", // The text line to be displayed
                "Agent", // Name of the character to be displayed as a part of this line
                             // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {

                        new ChoiceUnit(
                            "Expose family",
                            45,
                            new List<string>(){}
                            ),

                        new ChoiceUnit(
                            "Expose aristocrat",
                            39,
                            new List<string>(){}
                            ),

                        new ChoiceUnit(
                            "Expose both",
                            51,
                            new List<string>(){}
                            )
                }

                )
            );

        }
        else if (flags["Family"] == false && flags["Aristocrat"] == false)
        {
            dialogues.Add(
            // dialogue 27
            new DialogueUnit(

                "Captain, may I have a word?", // The text line to be displayed
                "Agent", // Name of the character to be displayed as a part of this line
                             // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {
                        new ChoiceUnit(
                            "Continue",
                            57,
                            new List<string>(){}
                            )
                }

                )
            );
        }

        // only family
            dialogues.Add(
                        // dialogue 28
                        new DialogueUnit(

                            "There is something I need to tell you, there is a stowaway family of Meludes on board. They snuck in and haven’t given me any trouble yet, I couldn’t turn them away.", // The text line to be displayed
                            "Captain", // Name of the character to be displayed as a part of this line
                                       // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        29,
                                        new List<string>(){}
                                        )
                            }

                            )
                        );
            dialogues.Add(
            // dialogue 29
            new DialogueUnit(

                "Captain, you should know that smuggling is a serious crime. I will have to take the family into custody and investigate the matter further.", // The text line to be displayed
                "Agent", // Name of the character to be displayed as a part of this line
                             // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {
                        new ChoiceUnit(
                            "Continue",
                            30,
                            new List<string>(){}
                            )
                }

                )
            );

            dialogues.Add(
            // dialogue 30
            new DialogueUnit(

                "I understand, Agent. I'm willing to accept the consequences of my actions. Just please make sure the family is treated fairly", // The text line to be displayed
                "Captain", // Name of the character to be displayed as a part of this line
                           // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {
                        new ChoiceUnit(
                            "Continue",
                            31,
                            new List<string>(){}
                            )

                }

                )
            );

        if (flags["Family"] == true && flags["Aristocrat"] == true && flags["Same place"] == false)
        {
             dialogues.Add(
            // dialogue 31
            new DialogueUnit(

                "Thank you for your cooperation Captain, but I will not deviate from the Republic's laws.", // The text line to be displayed
                "Agent", // Name of the character to be displayed as a part of this line
                             // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {
                        new ChoiceUnit(
                            "Continue",
                            67, // next part
                            new List<string>(){}
                            )

                }

                )
            );
        }
        else
        {
            dialogues.Add(
           // dialogue 31
           new DialogueUnit(

               "Thank you for your cooperation Captain, but I will not deviate from the Republic's laws.", // The text line to be displayed
               "Agent", // Name of the character to be displayed as a part of this line
                        // Now we create a list of all possible replies
               new List<ChoiceUnit>()
               {
                        new ChoiceUnit(
                            "Continue",
                            71, // next part
                            new List<string>(){}
                            )

               }

               )
           );
        }
           

            dialogues.Add(
                    // dialogue 32
                    new DialogueUnit(

                        "I found some stowaways, it is obvious you are not capable of controlling your own vessel, when I make my report your designation will not be much longer.", // The text line to be displayed
                        "Agent", // Name of the character to be displayed as a part of this line
                                     // Now we create a list of all possible replies
                        new List<ChoiceUnit>()
                        {
                        new ChoiceUnit(
                            "I am sorry sir...",
                            30,
                            new List<string>(){}
                            )
                        }

                        )
                    );


            // only aristocrat on board

            dialogues.Add(
                // dialogue 33
                new DialogueUnit(

                    "There is something I need to tell you, a Telite is on board. He demanded passage and I could not refuse, you know how these aristocrats can be.", // The text line to be displayed
                    "Captain", // Name of the character to be displayed as a part of this line
                               // Now we create a list of all possible replies
                    new List<ChoiceUnit>()
                    {
                        new ChoiceUnit(
                            "Continue",
                            34,
                            new List<string>(){}
                            )
                    }

                    )
                );

            dialogues.Add(
                // dialogue 34
                new DialogueUnit(

                    "Captain, you should know that smuggling is a serious crime, especially if it involves the likes of them. I will need to investigate the matter further.", // The text line to be displayed
                    "Agent", // Name of the character to be displayed as a part of this line
                                 // Now we create a list of all possible replies
                    new List<ChoiceUnit>()
                    {
                        new ChoiceUnit(
                            "Continue",
                            35,
                            new List<string>(){}
                            )
                    }

                    )
                );

            dialogues.Add(
                // dialogue 35
                new DialogueUnit(

                    "I understand, Agent. I'm willing to accept the consequences of my actions. Just don’t tell him you got this information from me.", // The text line to be displayed
                    "Captain", // Name of the character to be displayed as a part of this line
                               // Now we create a list of all possible replies
                    new List<ChoiceUnit>()
                    {
                        new ChoiceUnit(
                            "Continue",
                            36,
                            new List<string>(){}
                            )

                    }

                    )
                );

        if (flags["Family"] == true && flags["Aristocrat"] == true && flags["Same place"] == false)
        {
            dialogues.Add(
               // dialogue 36
               new DialogueUnit(

                   "In the light of that I will probably only leave you with a fine and a warning that the next time this happens you are going to lose a lot more than just some profits.", // The text line to be displayed
                   "Agent", // Name of the character to be displayed as a part of this line
                            // Now we create a list of all possible replies
                   new List<ChoiceUnit>()
                   {
                        new ChoiceUnit(
                            "Continue",
                            68, // next part
                            new List<string>(){}
                            )

                   }

                   )
               );
        }
        else
        {
             dialogues.Add(
                // dialogue 36
                new DialogueUnit(

                    "In the light of that I will probably only leave you with a fine and a warning that the next time this happens you are going to lose a lot more than just some profits.", // The text line to be displayed
                    "Agent", // Name of the character to be displayed as a part of this line
                                 // Now we create a list of all possible replies
                    new List<ChoiceUnit>()
                    {
                        new ChoiceUnit(
                            "Continue",
                            72, // next part
                            new List<string>(){}
                            )

                    }

                    )
                );
        }

           

            dialogues.Add(
                // dialogue 37
                new DialogueUnit(

                    "I found some stowaways, it is obvious you are not capable of controlling your own vessel, when I make my report your designation will not be much longer.", // The text line to be displayed
                    "Agent", // Name of the character to be displayed as a part of this line
                                 // Now we create a list of all possible replies
                    new List<ChoiceUnit>()
                    {
                        new ChoiceUnit(
                            "I am sorry sir, ",
                            38,
                            new List<string>(){}
                            )
                    }

                    )
                );

            dialogues.Add(
                // dialogue 38
                new DialogueUnit(

                    "At your age you should know better than this, maybe it is time for you to retire before any accidents befall you, these waters can be dangerous.", // The text line to be displayed
                    "Agent", // Name of the character to be displayed as a part of this line
                                 // Now we create a list of all possible replies
                    new List<ChoiceUnit>()
                    {
                        new ChoiceUnit(
                            "Continue",
                            34, // end of the game
                            new List<string>(){}
                            )

                    }

                    )
                );

            // same place on board

        // give up aristocrat
            dialogues.Add(
                // dialogue 39
                new DialogueUnit(

                    "There is something I need to tell you, a Telite is on board. He demanded passage and I could not refuse, you know how these aristocrats can be.", // The text line to be displayed
                    "Captain", // Name of the character to be displayed as a part of this line
                               // Now we create a list of all possible replies
                    new List<ChoiceUnit>()
                    {
                        new ChoiceUnit(
                            "Continue",
                            40,
                            new List<string>(){}
                            )
                    }

                    )
                );

            dialogues.Add(
                // dialogue 40
                new DialogueUnit(

                    "Captain, you should know that smuggling is a serious crime, especially if it involves the likes of them. I will need to investigate the matter further.", // The text line to be displayed
                    "Agent", // Name of the character to be displayed as a part of this line
                                 // Now we create a list of all possible replies
                    new List<ChoiceUnit>()
                    {
                        new ChoiceUnit(
                            "Continue",
                            41,
                            new List<string>(){}
                            )
                    }

                    )
                );

            dialogues.Add(
                // dialogue 41
                new DialogueUnit(

                    "I understand, Agent. I'm willing to accept the consequences of my actions. Just don’t tell him you got this information from me.", // The text line to be displayed
                    "Captain", // Name of the character to be displayed as a part of this line
                               // Now we create a list of all possible replies
                    new List<ChoiceUnit>()
                    {
                        new ChoiceUnit(
                            "Continue",
                            42,
                            new List<string>(){}
                            )

                    }

                    )
                );

            dialogues.Add(
                // dialogue 42
                new DialogueUnit(

                    "In the light of that I will probably only leave you with a fine and a warning that the next time this happens you are going to lose a lot more than just some profits.", // The text line to be displayed
                    "Agent", // Name of the character to be displayed as a part of this line
                                 // Now we create a list of all possible replies
                    new List<ChoiceUnit>()
                    {
                        new ChoiceUnit(
                            "Continue",
                            43,
                            new List<string>(){}
                            )

                    }

                    )
                );

            dialogues.Add(
                // dialogue 43
                new DialogueUnit(

                    "Captain, taking a Telite on board is one matter, however I found the Meludes family... lying to an official of the Kothos Republic is tantamount to treason.", // The text line to be displayed
                    "Agent", // Name of the character to be displayed as a part of this line
                                 // Now we create a list of all possible replies
                    new List<ChoiceUnit>()
                    {
                        new ChoiceUnit(
                            "I am sorry sir... ",
                            44,
                            new List<string>(){}
                            )
                    }

                    )
                );

            dialogues.Add(
                // dialogue 44
                new DialogueUnit(

                    "It is my duty to arrest you for your involvement in these crimes. Some time in the correct facility in Karthal might put you in the right path again, if you survive.", // The text line to be displayed
                    "Agent", // Name of the character to be displayed as a part of this line
                                 // Now we create a list of all possible replies
                    new List<ChoiceUnit>()
                    {
                        new ChoiceUnit(
                            "Continue",
                            70, // end of the game
                            new List<string>(){}
                            )

                    }

                    )
                );

        // give up family

        dialogues.Add(
                        // dialogue 45
                        new DialogueUnit(

                            "There is something I need to tell you, there is a stowaway family of Meludes on board. They snuck in / asked for passage and haven’t given me any trouble yet, I couldn’t turn them away.", // The text line to be displayed
                            "Captain", // Name of the character to be displayed as a part of this line
                                       // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        46,
                                        new List<string>(){}
                                        )
                            }

                            )
                        );
        dialogues.Add(
        // dialogue 46
        new DialogueUnit(

            "Captain, you should know that smuggling is a serious crime. I will have to take the family into custody and investigate the matter further.", // The text line to be displayed
            "Agent", // Name of the character to be displayed as a part of this line
                         // Now we create a list of all possible replies
            new List<ChoiceUnit>()
            {
                        new ChoiceUnit(
                            "Continue",
                            47,
                            new List<string>(){}
                            )
            }

            )
        );

        dialogues.Add(
        // dialogue 47
        new DialogueUnit(

            "I understand, Agent. I'm willing to accept the consequences of my actions. Just please make sure the family is treated fairly", // The text line to be displayed
            "Captain", // Name of the character to be displayed as a part of this line
                       // Now we create a list of all possible replies
            new List<ChoiceUnit>()
            {
                        new ChoiceUnit(
                            "Continue",
                            48,
                            new List<string>(){}
                            )

            }

            )
        );


        dialogues.Add(
        // dialogue 48
        new DialogueUnit(

            "Thank you for your cooperation Captain, but I will not deviate from the Republic's laws.", // The text line to be displayed
            "Agent", // Name of the character to be displayed as a part of this line
                         // Now we create a list of all possible replies
            new List<ChoiceUnit>()
            {
                        new ChoiceUnit(
                            "Continue",
                            49, // next part
                            new List<string>(){}
                            )

            }

            )
        );

        dialogues.Add(
                // dialogue 49
                new DialogueUnit(

                    "Captain, taking a  Meludes family on board is one matter, however I found the Telite aristocrat... lying to an official of the Kothos Republic is tantamount to treason.", // The text line to be displayed
                    "Agent", // Name of the character to be displayed as a part of this line
                                 // Now we create a list of all possible replies
                    new List<ChoiceUnit>()
                    {
                        new ChoiceUnit(
                            "I am sorry sir... ",
                            50,
                            new List<string>(){}
                            )
                    }

                    )
                );

        dialogues.Add(
            // dialogue 50
            new DialogueUnit(

                "It is my duty to arrest you for your involvement in these crimes. Some time in the correct facility in Karthal might put you in the right path again, if you survive.", // The text line to be displayed
                "Agent", // Name of the character to be displayed as a part of this line
                             // Now we create a list of all possible replies
                new List<ChoiceUnit>()
                {
                        new ChoiceUnit(
                            "Continue",
                            69, // end of the game
                            new List<string>(){}
                            )

                }

                )
            );

        // expose both

        dialogues.Add(
        // dialogue 51
        new DialogueUnit(

            "There is something I need to tell you. A Telite and a family of Meludes are on board. One demanded passage and the others were in such poor condition that I couldn’t refuse.", // The text line to be displayed
            "Captain", // Name of the character to be displayed as a part of this line
                       // Now we create a list of all possible replies
            new List<ChoiceUnit>()
            {
                        new ChoiceUnit(
                            "Continue",
                            52,
                            new List<string>(){}
                            )

            }

            )
        );


        dialogues.Add(
        // dialogue 52
        new DialogueUnit(

            "Captain, you should know that smuggling is a serious crime, especially if it involves the likes of them. I will have to take both the family and the aristocrat into custody and investigate the matter further.", // The text line to be displayed
            "Agent", // Name of the character to be displayed as a part of this line
                       // Now we create a list of all possible replies
            new List<ChoiceUnit>()
            {
                        new ChoiceUnit(
                            "Continue",
                            53,
                            new List<string>(){}
                            )

            }

            )
        );

        dialogues.Add(
       // dialogue 53
       new DialogueUnit(

           "I understand, Agent. I'm willing to accept the consequences of my actions. I would never disregard the Republics laws willingly, my hand was forced this time.", // The text line to be displayed
           "Captain", // Name of the character to be displayed as a part of this line
                      // Now we create a list of all possible replies
           new List<ChoiceUnit>()
           {
                        new ChoiceUnit(
                            "Continue",
                            54,
                            new List<string>(){}
                            )

           }

           )
       );

        dialogues.Add(
        // dialogue 54
        new DialogueUnit(

            "In light of the circumstances I have no choice other than commandeering this vessel.", // The text line to be displayed
            "Agent", // Name of the character to be displayed as a part of this line
                         // Now we create a list of all possible replies
            new List<ChoiceUnit>()
            {
                        new ChoiceUnit(
                            "Continue",
                            55,
                            new List<string>(){}
                            )

            }

            )
        );

        dialogues.Add(
        // dialogue 55
        new DialogueUnit(

            "I have contacted the local authorities so they can take both the family and the aristocrat into custody at our next stop.", // The text line to be displayed
            "Agent", // Name of the character to be displayed as a part of this line
                         // Now we create a list of all possible replies
            new List<ChoiceUnit>()
            {
                        new ChoiceUnit(
                            "Continue",
                            56,
                            new List<string>(){}
                            )

            }

            )
        );

        dialogues.Add(
        // dialogue 56
        new DialogueUnit(

            "You will be under my command for this mission, your sentence when we get back will depend on your performance.", // The text line to be displayed
            "Agent", // Name of the character to be displayed as a part of this line
                         // Now we create a list of all possible replies
            new List<ChoiceUnit>()
            {
                        new ChoiceUnit(
                            "Continue",
                            66, // end
                            new List<string>(){}
                            )

            }

            )
        );

        // none on board

        dialogues.Add(
                        // dialogue 57
                        new DialogueUnit(

                            "Of course, are you comfortable in the quarters? Is there anything I can do for you?", // The text line to be displayed
                            "Captain", // Name of the character to be displayed as a part of this line
                                       // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        58,
                                        new List<string>(){}
                                        )
                            }

                            )
                        );

        dialogues.Add(
        // dialogue 58
        new DialogueUnit(

            "I conducted an inspection of your vessel. Several of your safety protocols are out of date, and some of your equipment is not properly maintained. This is a serious breach of regulation in a government contracted submarine.", // The text line to be displayed
            "Agent", // Name of the character to be displayed as a part of this line
                         // Now we create a list of all possible replies
            new List<ChoiceUnit>()
            {
                        new ChoiceUnit(
                            "Continue",
                            59, // end
                            new List<string>(){}
                            )

            }

            )
        );

        dialogues.Add(
                        // dialogue 59
                        new DialogueUnit(

                            "I'm sorry, Agent. We have been working hard to keep our ship in good condition, but I see now that we have fallen behind on some of our normal procedures, these are not easy waters to navigate.", // The text line to be displayed
                            "Captain", // Name of the character to be displayed as a part of this line
                                       // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        60,
                                        new List<string>(){}
                                        )
                            }

                            )
                        );

        dialogues.Add(
        // dialogue 61
        new DialogueUnit(

            "Captain, you should know that violating safety codes is a serious offence. I am required to issue a fine for these violations. Perhaps an arrangement can be made as I need transportation beyond Belium Station.", // The text line to be displayed
            "Agent", // Name of the character to be displayed as a part of this line
                         // Now we create a list of all possible replies
            new List<ChoiceUnit>()
            {
                        new ChoiceUnit(
                            "Continue",
                            62, // end
                            new List<string>(){}
                            )

            }

            )
        );

        dialogues.Add(
                        // dialogue 62
                        new DialogueUnit(

                            "As you see we have some repairs scheduled at the station, maybe if you help us field the costs we would be able to better accommodate your needs.", // The text line to be displayed
                            "Captain", // Name of the character to be displayed as a part of this line
                                       // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        63,
                                        new List<string>(){}
                                        )
                            }

                            )
                        );

        dialogues.Add(
        // dialogue 63
        new DialogueUnit(

            "I will talk to the quartermaster when we get there to see what can be done. However this will further your debt to me.", // The text line to be displayed
            "Agent", // Name of the character to be displayed as a part of this line
                         // Now we create a list of all possible replies
            new List<ChoiceUnit>()
            {
                        new ChoiceUnit(
                            "That would make me more hopeful for a journey in the Scarred Depths.",
                            64, // end
                            new List<string>(){}
                            ),

                        new ChoiceUnit(
                            "Maybe when we get back then.",
                            65, // end
                            new List<string>(){}
                            )
            }

            )
        );


        // Endings //
        
        // ending 8 
        // no F or A, accept reapirs
        dialogues.Add(
                        // dialogue 64
                        new DialogueUnit(

                            "The Captain was conscripted by the Agent, hopfully the repairs to the vessel help them in their endeavour. Will they succeed?", // The text line to be displayed
                            "Narrator", // Name of the character to be displayed as a part of this line
                                       // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        73, // end sentence
                                        new List<string>(){}
                                        )
                            }

                            )
                        );
        // ending 9
        // no F or A, dont accept repairs
        dialogues.Add(
                        // dialogue 65
                        new DialogueUnit(

                            "The Captain was conscripted by the Agent, hopfully the vessels state will not lead to an under pressure swim at the Scarred Depths. Will they survive?", // The text line to be displayed
                            "Narrator", // Name of the character to be displayed as a part of this line
                                        // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        73, // end sentence
                                        new List<string>(){}
                                        )
                            }

                            )
                        );
        // give up both
        dialogues.Add(
                        // dialogue 66
                        new DialogueUnit(

                            "The Captain is arrested and sent to a correctional facility, will he be able to escape?", // The text line to be displayed
                            "Narrator", // Name of the character to be displayed as a part of this line
                                        // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        73, // end sentence
                                        new List<string>(){}
                                        )
                            }

                            )
                        );

        //  give up family, both on board, dif places
        dialogues.Add(
                        // dialogue 67
                        new DialogueUnit(

                            "The Aristocrat flees. The Captain is arrested and sent to a workers camp along with the family. Will their sick family member survive? How will they escape?", // The text line to be displayed
                            "Narrator", // Name of the character to be displayed as a part of this line
                                        // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        73, // end sentence
                                        new List<string>(){}
                                        )
                            }

                            )
                        );
        // give up A, both on board, dif places
        dialogues.Add(
                        // dialogue 68
                        new DialogueUnit(

                            "The Aristocrat is taken by the Agent. The family manages to escape. The Captain is arrested and sent to a workers camp, will he be able to escape?", // The text line to be displayed
                            "Narrator", // Name of the character to be displayed as a part of this line
                                        // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        73, // end sentence
                                        new List<string>(){}
                                        )
                            }

                            )
                        );

        // both on board, give up F, same place

        dialogues.Add(
                        // dialogue 69
                        new DialogueUnit(

                            "The Aristocrats is taken by the Agent. The Captain is arrested and sent to a workers camp along with the family. Will their sick family member survive? How will they escape?", // The text line to be displayed
                            "Narrator", // Name of the character to be displayed as a part of this line
                                        // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        73, // end sentence
                                        new List<string>(){}
                                        )
                            }

                            )
                        );

        // both on board, give up A, same place
        dialogues.Add(
                        // dialogue 70
                        new DialogueUnit(

                            "The Aristocrat is taken by the Agent. The Captain is arrested and sent to a workers camp with the family, will they be able to escape?", // The text line to be displayed
                            "Narrator", // Name of the character to be displayed as a part of this line
                                        // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        73, // end sentence
                                        new List<string>(){}
                                        )
                            }

                            )
                        );

        // only F, give up
        dialogues.Add(
                        // dialogue 71
                        new DialogueUnit(

                            "The Captain is arrested and sent to a workers camp along with the family. Will their sick family member survive? How will they escape?", // The text line to be displayed
                            "Narrator", // Name of the character to be displayed as a part of this line
                                        // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        73, // end sentence
                                        new List<string>(){}
                                        )
                            }

                            )
                        );

        // only A, give up
        dialogues.Add(
                        // dialogue 72
                        new DialogueUnit(

                            "The Aristocrat is taken by the Agent. The Captain is arrested and sent to a workers camp, will he be able to escape?", // The text line to be displayed
                            "Narrator", // Name of the character to be displayed as a part of this line
                                        // Now we create a list of all possible replies
                            new List<ChoiceUnit>()
                            {
                                    new ChoiceUnit(
                                        "Continue",
                                        73, // end sentence
                                        new List<string>(){}
                                        )
                            }

                            )
                        );

        dialogues.Add(
                       // dialogue 73
                       new DialogueUnit(

                           "If you want to find out play the rest of the game!", // The text line to be displayed
                           "Narrator", // Name of the character to be displayed as a part of this line
                                       // Now we create a list of all possible replies
                           new List<ChoiceUnit>()
                           {
                                    new ChoiceUnit(
                                        "Continue",
                                        0, // end sentence
                                        new List<string>(){}
                                        )
                           }

                           )
                       );
    }
}


