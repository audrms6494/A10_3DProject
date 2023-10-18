using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LaunchObject : MonoBehaviour, IObject
{
    [SerializeField] public float timeRate;
    [SerializeField] public int force;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] public Vector3 direction;
    [SerializeField] public GameObject prefab;
    [SerializeField] public int poolSize = 10; // Ǯ ������

    private List<GameObject> objectPool; // ������Ʈ Ǯ
    private Rigidbody _rigidbody;


    void Start()
    {
        // ������Ʈ Ǯ �ʱ�ȭ
        objectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
        direction = direction.normalized;
    }

    public virtual void Use()
    {
        StartCoroutine(Fire());
    }

    // ������Ʈ Ǯ���� ������Ʈ ��������
    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // Ǯ���� ��Ȱ��ȭ�� ������Ʈ�� ������ ���� �����Ͽ� ��ȯ
        GameObject newObj = Instantiate(prefab);
        objectPool.Add(newObj);
        return newObj;
    }

    // ������Ʈ�� Ǯ�� ��ȯ�ϱ�
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    public IEnumerator Fire()
    {
        GameObject obj = GetObjectFromPool();
        obj.GetComponent<Transform>().position = spawnPoint.position;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(direction * force, ForceMode.Impulse);
        Debug.Log("spawn!");
        yield return new WaitForSeconds(timeRate);

        StartCoroutine(Destroy(obj));
        StartCoroutine(Fire());
    }

    public IEnumerator Destroy(GameObject obj)
    {
        yield return new WaitForSeconds(10f);
        ReturnObjectToPool(obj);
    }
}
