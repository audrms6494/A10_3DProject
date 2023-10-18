using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour, IInteractionObject
{
    [SerializeField] public GameObject activateObj;

    public void Use()
    {
        if(CheckCondition())
            activateObj.SetActive(true);
    }

    public bool CheckCondition()
    {
        return true;
    }


}
