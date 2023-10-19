using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColingLava : OnOffObject
{
    [SerializeField] List<GameObject> ConditionObjects;

    public override bool CheckCondition()
    {
        for(int i = 0; i< ConditionObjects.Count; i++)
        {
            if (!ConditionObjects[i].activeSelf) 
            { 
                return false;
            }
        }
        return true;
    }
}
