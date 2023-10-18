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
    [SerializeField] public int poolSize = 10; // 풀 사이즈

    private List<GameObject> objectPool; // 오브젝트 풀
    private Rigidbody _rigidbody;


    void Start()
    {
        // 오브젝트 풀 초기화
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

    // 오브젝트 풀에서 오브젝트 가져오기
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

        // 풀에서 비활성화된 오브젝트가 없으면 새로 생성하여 반환
        GameObject newObj = Instantiate(prefab);
        objectPool.Add(newObj);
        return newObj;
    }

    // 오브젝트를 풀에 반환하기
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
