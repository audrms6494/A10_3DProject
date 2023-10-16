using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InActionObject
{
    [SerializeField] public bool leftSide = false; // left = false, right = true
    public bool isOpen = false;



    public override IEnumerator Move()
    {
        isActive = true;

        Vector3 startingPos = transform.position;
        Vector3 targetPos;

        if (isOpen)
            if (leftSide)
                targetPos = rightPosition.position;
            else
                targetPos = leftPosition.position;
        else
            if (leftSide)
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
