using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class UpDownCycleObject : MonoBehaviour, IMovingObject
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed; // �̵� �ӵ�
    [SerializeField] public Transform destination; // ������
    [SerializeField] public float movedTime; //�� �ð�.

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


        // �־��� �ð� ���� �ε巴�� �̵�
        while (elapsedTime < movedTime)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        direction = !direction;

        elapsedTime = 0f;
        // �̵� �Ϸ� �� �߰� �۾� ���� ����
        isMoving = false;
        StartCoroutine(Move());
    }

}
