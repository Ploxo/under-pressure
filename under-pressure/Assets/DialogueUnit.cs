using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueUnit
{

    //[SerializeField] public int id;
    [SerializeField] public string text;
    [SerializeField] public string character;

    [SerializeField] public List<ChoiceUnit> choices;

    public DialogueUnit(/*int newId,*/ string newText, string newCharacter, List<ChoiceUnit> newChoices){

    // id = newId;
    text = newText;
    character = newCharacter;
    choices = newChoices;

    }

    
}
