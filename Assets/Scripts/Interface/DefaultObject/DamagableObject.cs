using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObject : MonoBehaviour, IDamagableObject
{
    [SerializeField] public int damage;

    private static readonly int Hit = Animator.StringToHash("Hit");


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
        character.transform.GetChild(0).GetComponent<Animator>().SetTrigger(Hit);
        // ���
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            OnDamaged(other.gameObject);
    }
}
