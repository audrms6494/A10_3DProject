using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(100f, 0.5f, 17.5f), 0.01f);
        if (transform.position.x > 5)
        {
            transform.position = new Vector3(-5, 0.5f, 17.5f);
        }
    }
}
