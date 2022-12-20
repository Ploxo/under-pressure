using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueUnit
{

    [SerializeField] public int _id;
    [SerializeField] public string _text;

    [SerializeField] public List<ChoiceUnit> _choices;

    public int Id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value ;
        }
    }

    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            _text = value ;
        }
    }

    public List<ChoiceUnit> Choices
    {
        get
        {
            return _choices;
        }
        set
        {
            _choices = value ;
        }
    }

    public DialogueUnit(int id, string text, List<ChoiceUnit> choices){

    _id = id;
    _text = text;
    _choices = choices;

    }

    
}
