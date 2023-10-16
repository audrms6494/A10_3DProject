using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InActionObject : MonoBehaviour, IMovingObject
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed; // �̵� �ӵ�
    [SerializeField] public Transform leftPosition; // ���� �̵�
    [SerializeField] public Transform rightPosition; // ������ �̵�

    protected float elapsedTime = 0f;
    public bool isActive = false;
    
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

    public virtual IEnumerator Move()
    {
        isActive = true;

        yield return null;

        isActive = false;
        StopCoroutine(Move());
    }
}
