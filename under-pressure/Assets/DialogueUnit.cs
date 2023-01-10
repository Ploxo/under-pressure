using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueUnit
{

    //[SerializeField] public int id;
    [TextArea(10, 10)]
    public string text;
    public string character;

    public List<ChoiceUnit> choices;

    public DialogueUnit(/*int newId,*/ string newText, string newCharacter, List<ChoiceUnit> newChoices){

    // id = newId;
    text = newText;
    character = newCharacter;
    choices = newChoices;

    }

    
}
