using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChoiceUnit
{
    // [SerializeField] public int _id;
    [SerializeField] public string _text;
    [SerializeField] public int _nextDialogueUnit;
    [SerializeField] public List<string> _flags;

    // public int Id
    // {
    //     get
    //     {
    //         return _id;
    //     }
    //     set
    //     {
    //         _id = value ;
    //     }
    // }

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

        public int NextDialogueUnit
    {
        get
        {
            return _nextDialogueUnit;
        }
        set
        {
            _nextDialogueUnit = value ;
        }
    }

    public List<string> Flags
    {
        get
        {
            return _flags;
        }
        set
        {
            _flags = value ;
        }
    }

    public ChoiceUnit(/*int id,*/ string text, int nextDialogueUnit, List<string> flags){

        // _id = id;
        _text = text;
        _nextDialogueUnit = nextDialogueUnit;
        _flags = flags;

    }

    public void onChoice(){
        return;
    }

}
