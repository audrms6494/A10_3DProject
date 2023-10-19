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

    protected virtual void OnDamaged(GameObject character)
    {
        character.GetComponent<CharacterHealth>().TakeDamage(damage);

        // animation ó��
        // ���
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            OnDamaged(other.gameObject);
    }
}
