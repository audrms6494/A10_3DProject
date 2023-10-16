using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractionObject
{
    [SerializeField] GameObject ConnectedObject;


    IObject connectedObject;

    private void Awake()
    {
        connectedObject = ConnectedObject.GetComponent<IObject>();
    }


    public void Use()
    {
        if(CheckCondition())
            connectedObject.Use();
    }

    public bool CheckCondition()
    {
        throw new System.NotImplementedException();
    }

}
