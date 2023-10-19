using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPosition, 0.01f);
        if(transform.position == endPosition)
        {
            transform.position = startPosition;
        }
    }
}
