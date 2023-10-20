using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static DataManager Data { get => Instance._data; }
    [SerializeField] private GameObject PlayerPrefab;
    public GameObject Player { get; private set; }
    public bool[] StageClearFlags = new bool[3];

    [SerializeField] private DataManager _data;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        SoundManager.ChangeBGM(0);
    }

    private void LateUpdate()
    {
        if (Player == null)
            Player = GameObject.FindWithTag(PlayerPrefab.tag);
    }
}
