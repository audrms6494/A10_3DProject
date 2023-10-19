using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour, IInteractable
{
    [SerializeField] private float Speed;
    [SerializeField] private List<Transform> movingPos;
    [SerializeField] private LayerMask _interactLayer;
    private Transform _player;

    public string GetInteractPrompt()
    {
        return "</b>[E]</b>\n¿Ãµø";
    }

    public void OnInteract()
    {
        if (_player != null)
        {
            Move(_player);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_interactLayer.value == (other.gameObject.layer & _interactLayer.value))
        {
            _player = other.gameObject.GetComponent<Transform>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _player = null;
    }

    public void Move(Transform player)
    {
        StartCoroutine(Moving(player));
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
