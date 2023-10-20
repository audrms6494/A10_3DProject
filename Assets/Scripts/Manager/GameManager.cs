using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static DataManager Data { get => Instance._data; }
    public GameObject Player;

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

    private void Update()
    {
        if (Player == null)
            Player = GameObject.Find(nameof(Player));
    }
}
