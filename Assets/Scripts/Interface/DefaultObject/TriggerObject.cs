using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : OnOffObject , ITriggerObject
{
    public void OnTriggerEnter(Collider other)
    {
        Use();
    }

}
