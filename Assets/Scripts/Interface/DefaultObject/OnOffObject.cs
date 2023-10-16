using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffObject : MonoBehaviour, IInteractionObject
{
    [SerializeField] List<GameObject> ConnectedObject;

    IObject connectedObject;



    public void Use()
    {
        if (CheckCondition())
        {
            foreach (GameObject obj in ConnectedObject)
            {
                connectedObject = obj.GetComponent<IObject>();
                connectedObject.Use();
            }
        }
    }

    public bool CheckCondition()
    {
        return true;
    }

}
