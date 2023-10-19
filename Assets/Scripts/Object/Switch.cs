using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : OnOffObject, IInteractable
{
    [SerializeField] public string Name;
    [SerializeField] GameObject checkObj;


    public string GetInteractPrompt()
    {
        return Name;
    }

    public void OnInteract()
    {
        if(CheckCondition())
            Use();
    }

    public override bool CheckCondition()
    {
        if(checkObj.activeSelf)
            return true;
        else
            return false;
    }
}
