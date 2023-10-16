using System.Collections;
using System.Collections.Generic;
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
