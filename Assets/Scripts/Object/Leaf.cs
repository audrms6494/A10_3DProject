using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public bool gameStart;
    public void Update()
    {
        if (gameStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 50f), 0.01f);
        }
    }
}
