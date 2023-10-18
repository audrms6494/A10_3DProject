using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class UpDownCycleObject : MonoBehaviour, IMovingObject
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed; // 이동 속도
    [SerializeField] public Transform destination; // 목적지
    [SerializeField] public float movedTime; //편도 시간.

    private float elapsedTime; 
    private bool direction = true;
    private bool isMoving = false;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = destination.position - (destination.position - transform.position);
    }

    private void Start()
    {
        Use();
    }

    public void Use()
    {
        if(CheckCondition())
            StartCoroutine(Move());
    }

    public bool CheckCondition()
    {
        return true;
    }


    public virtual IEnumerator Move()
    {
        //Debug.Log(startPosition);
        isMoving = true;

        Vector3 startingPos = transform.position;
        Vector3 targetPos;

        if (direction)
        {
            targetPos = destination.position;
        }
        else
        {
            targetPos = startPosition;
        }


        // 주어진 시간 동안 부드럽게 이동
        while (elapsedTime < movedTime)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        direction = !direction;

        elapsedTime = 0f;
        // 이동 완료 후 추가 작업 수행 가능
        isMoving = false;
        StartCoroutine(Move());
    }

}
