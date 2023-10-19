using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObject : MonoBehaviour, IDamagableObject
{
    [SerializeField] public int damage;

    private static readonly int Hit = Animator.StringToHash("Hit");


    public virtual void Use()
    {
        // 기능
        // 함정 작동
        // 함정 무효화 <함정 끄기.>
    }

    protected virtual void OnDamaged(GameObject character)
    {
        character.GetComponent<CharacterHealth>().TakeDamage(damage);

        // animation 처리
        character.transform.GetChild(0).GetComponent<Animator>().SetTrigger(Hit);
        // 등등
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            OnDamaged(other.gameObject);
    }
}
