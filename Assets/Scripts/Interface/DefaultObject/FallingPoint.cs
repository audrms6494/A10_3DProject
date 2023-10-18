using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPoint : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] Transform SpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharacterHealth>().TakeDamage(damage);
            other.gameObject.transform.position = SpawnPoint.position;
            other.gameObject.transform.rotation = SpawnPoint.rotation;
        }
    }

}
