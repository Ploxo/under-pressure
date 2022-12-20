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
        
            // dialogues = new List<DialogueUnit>();

            // flags = new Dictionary<string, bool>(){

            //     {"is_polite", false},

            // };

            // // Adding a new dialogue line
            // dialogues.Add(

            //     // This is a dialogue unit, holding both the line of text, and the possible replies
            //     new DialogueUnit(
            //         0, // The id of this dialogue unit
            //         "Hello! Last delivery went prety well, I have to say!", // The text line to be displayed
            //         // Now we create a list of all possible replies
            //         new List<ChoiceUnit>(){
            //             // First option
            //             new ChoiceUnit(
            //                 0, // Id of this option
            //                 "Thanks!", // The text of this reply option
            //                 1, // The id of the next dialogue unit if this line is chosen
            //                 new List<string>(){"is_polite"} // Flags for the Dialogue Manager to set if this line is chosen
            //             ),
            //             new ChoiceUnit(
            //                 1,
            //                 "Yeah, no thanks to you!",
            //                 2,
            //                 new List<string>(){""}
            //             )

            //         }
            //     )

            // );

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
