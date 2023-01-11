using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListDialogues : MonoBehaviour
{
    [SerializeField]
    DialogueManager manager;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (var dialogue in manager.dialogues)
        {
            foreach (var choice in dialogue.choices)
            {
                if (CheckEnd(choice))
                {
                    Debug.Log(i);
                }
            }
            i++;
        }
    }

    private bool CheckEnd(ChoiceUnit unit)
    {
        foreach (string flag in unit.flags)
            if (flag.Contains("end"))
                return true;

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
