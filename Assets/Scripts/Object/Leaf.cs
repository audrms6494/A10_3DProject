using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public bool gameStart;
    public Vector3 destinationPoisition;
    public void Update()
    {
        if (gameStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPoisition, 0.01f);
        }
    }
}
