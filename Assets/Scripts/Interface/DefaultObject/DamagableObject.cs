using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObject : MonoBehaviour, IDamagableObject
{
    [SerializeField] public int damage;


    public virtual void Use()
    {
        // ���
        // ���� �۵�
        // ���� ��ȿȭ <���� ����.>
    }

    protected virtual void Damaged(GameObject character)
    {
        character.GetComponent<CharacterHealth>().TakeDamage(damage);

        // animation ó��
        // ���
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        Damaged(other.gameObject);
    }
}
