using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pipe : MonoBehaviour, IInteractable
{
    [SerializeField] private float Speed;
    [SerializeField] private List<Transform> movingPos;
    CapsuleCollider capsuleCollider;
    PlayerInput input;
    Rigidbody rb;
    public string GetInteractPrompt()
    {
        return "¿Ãµø";
    }

    public void OnInteract()
    {
        capsuleCollider = GameManager.Instance.Player.GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;
        input = GameManager.Instance.Player.GetComponent<PlayerInput>();
        input.enabled = false;
        rb = GameManager.Instance.Player.GetComponent<Rigidbody>();
        rb.useGravity = false;
        Move(GameManager.Instance.Player.transform.GetChild(0));
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
            Vector3 dir = (targetPos - obj.transform.position).normalized;
            rb.velocity = dir * Speed;
            //obj.transform.position += dir * Speed * Time.deltaTime;

            if ((obj.transform.position - targetPos).magnitude < 0.1f)
            {
                obj.transform.position = targetPos;
                if (i >= movingPos.Count) break;
                targetPos = movingPos[i++].position;
            }
            yield return null;
        }
        capsuleCollider.enabled = true;
        rb.useGravity = true;
        input.enabled = true;
    }
}
