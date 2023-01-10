using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChoiceUnit
{
    // [SerializeField] public int _id;
    [TextArea(10, 10)]
    public string text;
    public int nextDialogueUnit;
    public List<string> flags;

    public ChoiceUnit(/*int id,*/ string newText, int newNextDialogueUnit, List<string> newFlags){

        // _id = id;
        text = newText;
        nextDialogueUnit = newNextDialogueUnit;
        flags = newFlags;

    }

    public void onChoice(){
        return;
    }

}
