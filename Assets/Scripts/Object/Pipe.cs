using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour, IInteractable
{
    [SerializeField] private ePipeType PipeType;
    [SerializeField] private float Speed;
    [SerializeField] private List<Transform> movingPos;

    public string GetInteractPrompt()
    {
        throw new System.NotImplementedException();
    }

    public void Move(Transform player)
    {
        StartCoroutine(Moving(player));
    }

    public void OnInteract()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator Moving(Transform player)
    {
        int i = 0;
        Vector3 targetPos = movingPos[i++].position;
        while (i < movingPos.Count)
        {
            // TODO
            Vector3 dir = (player.transform.position - targetPos).normalized;
            player.transform.position += dir * Speed * Time.deltaTime;

            if ((player.transform.position - targetPos).magnitude < 0.1f)
            {
                player.transform.position = targetPos;
                if (i >= movingPos.Count) break;
                targetPos = movingPos[i++].position;
            }
            yield return null;
        }
    }
}
