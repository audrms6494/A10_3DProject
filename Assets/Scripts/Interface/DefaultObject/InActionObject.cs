using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InActionObject : MonoBehaviour, IMovingObject
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed; // 이동 속도
    [SerializeField] public Transform leftPosition; // 왼쪽 이동
    [SerializeField] public Transform rightPosition; // 오른쪽 이동
    [SerializeField] public bool moveDirection = false; // left = false, right = true

    private float elapsedTime = 0f;
    public bool isOpen = false;
    public bool isActive = false;
    

    private void Update()
    {
        Debug.Log(isOpen);
    }

    public virtual void Use()
    {
        Debug.Log("Active");
        if(!isActive)
            StartCoroutine(Move());
    }

    public virtual bool CheckCondition()
    {
        return true;
    }

    IEnumerator Move()
    {
        isActive = true;

        Vector3 startingPos = transform.position;
        Vector3 targetPos;

        if (isOpen)
            if(moveDirection)
                targetPos = rightPosition.position;
            else
                targetPos = leftPosition.position;  
        else
            if(moveDirection)
                targetPos = leftPosition.position;
            else
                targetPos = rightPosition.position;


        // 주어진 시간 동안 부드럽게 이동
        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        if (isOpen)
            isOpen = false;
        else
            isOpen = true;

        elapsedTime = 0f;
        // 이동 완료 후 추가 작업 수행 가능
        Debug.Log("이동 완료!");
        isActive = false;
        StopCoroutine(Move());
    }
}
