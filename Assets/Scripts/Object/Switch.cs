using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : OnOffObject, IInteractable
{
    [SerializeField] public string Name;


    public string GetInteractPrompt()
    {
        return Name;
    }

    public void OnInteract()
    {
        Debug.Log("Active");
        Use();
    }
}
