using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour, IInteractable
{
    public Vector3 destinationPoisition;
    public bool gameStart;

    public string GetInteractPrompt()
    {
        string text = "Game Start";
        return text;
    }

    public void OnInteract()
    {
        gameStart = true;
        this.gameObject.layer = 0;
    }

    public void Update()
    {
        if (gameStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPoisition, 0.01f);
        }
    }
}
