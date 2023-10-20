using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonSpawner : MonoBehaviour
{
    private const string dragonPrefabPath = "Prefabs/Dragon/";
    private void Start()
    {
        bool[] clearFlag = GameManager.Instance.StageClearFlags;
        for (int i = 0; i < clearFlag.Length; i++)
        {
            if (clearFlag[i])
            {
                GameObject dragon = Resources.Load(dragonPrefabPath + $"Dragon{i + 1}") as GameObject;
                float x = Random.Range(-5f, 5f);
                float z = Random.Range(-5f, 5f);
                Vector3 position = new Vector3(x, 0, z);
                dragon = Instantiate(dragon, position, Quaternion.identity);
            }
        }
    }
}
