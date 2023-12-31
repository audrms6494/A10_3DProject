using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffObject : MonoBehaviour, IInteractionObject
{
    [SerializeField] List<GameObject> ConnectedObject;

    IObject connectedObject;

    public virtual void Use()
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

    public virtual bool CheckCondition()
    {
        return true;
    }

}
