using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IObject
{
    void Use();
}

public interface IMovingObject : IObject
{
    bool CheckCondition();
}

public interface IInteractionObject : IObject
{
    bool CheckCondition();
}

public interface ITrapObject : IObject
{
    void OnTriggerEnter(Collider other);
}