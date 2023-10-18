using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] public Vector3 rotationAxis = Vector3.up;
    [SerializeField] public int rotateSpeed = 1;

    void Start()
    {
        StartCoroutine(Rotation());
    }

    public IEnumerator Rotation()
    {
        while (true)
        {
            transform.Rotate(rotationAxis, Time.deltaTime * 50f * rotateSpeed, Space.World);
            yield return null;
        }
    }

}
