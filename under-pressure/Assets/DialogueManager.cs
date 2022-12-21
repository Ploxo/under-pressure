using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class DialogueManager : MonoBehaviour
{

    [SerializeField] public Dictionary<string, bool> flags;

    [SerializeField] public List<DialogueUnit> dialogues;

    // Start is called before the first frame update
    void Start()
    {
        
            dialogues = new List<DialogueUnit>();

            flags = new Dictionary<string, bool>(){

                {"likes_apples", false},

            };

            // Adding a new dialogue line
            dialogues.Add(

                // This is a dialogue unit, holding both the line of text, and the possible replies
                new DialogueUnit(

                    0, // The id of this dialogue unit
                    "Hey there! Do you like bananas or apples?", // The text line to be displayed
                    // Now we create a list of all possible replies

                    new List<ChoiceUnit>(){

                        // First option
                        new ChoiceUnit(

                            "Apples!", // The text of this reply option
                            1, // The id of the next dialogue unit if this line is chosen
                            new List<string>(){"likes_apples"} // Flags for the Dialogue Manager to set if this line is chosen

                        ),
                        // Second option
                        new ChoiceUnit(
                            "Bananas!",
                            2,
                            new List<string>(){""} // Empty list to show that no flags need to be set
                        )

                    }
                )

            );

            dialogues.Add(

                new DialogueUnit(
                    1,
                    "Me too! I love apples!",
                    new List<ChoiceUnit>(){
                        new ChoiceUnit(
                            "I'll get you some apples, then!",
                            3,
                            new List<string>(){""}
                        )
                    }
                )
            );

            dialogues.Add(

                new DialogueUnit(
                    2,
                    "Well, if you don't like bananas, I guess I'll get you some apples!",
                    new List<ChoiceUnit>(){
                        new ChoiceUnit(
                            "I'll get you some apples, then!",
                            3,
                            new List<string>(){""}
                        )
                    }
                )
            );

            dialogues.Add(

                new DialogueUnit(
                    3,
                    "Exactly, get me some god-damn apples!",
                    new List<ChoiceUnit>(){
                        new ChoiceUnit(
                            "This dialogue option is the last, and leads back to itself",
                            3,
                            new List<string>(){""}
                        )
                    }
                )
            );

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
