using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour, IInteractable
{
    [SerializeField] private float Speed;
    [SerializeField] private List<Transform> movingPos;
    [SerializeField] private LayerMask _interactLayer;

    public string GetInteractPrompt()
    {
        return "¿Ãµø";
    }

    public void OnInteract()
    {
        Move(GameManager.Instance.Player.transform);
    }

    public void Move(Transform obj)
    {
        StartCoroutine(Moving(obj));
    }

    private IEnumerator Moving(Transform obj)
    {
        int i = 0;
        Vector3 targetPos = movingPos[i++].position;
        while (i < movingPos.Count)
        {
            // TODO
            Vector3 dir = (obj.transform.position - targetPos).normalized;
            obj.transform.position += dir * Speed * Time.deltaTime;

            if ((obj.transform.position - targetPos).magnitude < 0.1f)
            {
                obj.transform.position = targetPos;
                if (i >= movingPos.Count) break;
                targetPos = movingPos[i++].position;
            }
            yield return null;
        }
    }
}
