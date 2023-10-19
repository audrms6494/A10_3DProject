using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCannon : ActivateObject, IInteractable
{
    [SerializeField] public string Name;

    public string GetInteractPrompt()
    {
        return Name;
    }

    public void OnInteract()
    {
        Use();
    }


}
