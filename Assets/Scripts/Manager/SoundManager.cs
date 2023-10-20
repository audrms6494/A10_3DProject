using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Pool;

public class SoundManager : MonoBehaviour
{
    [HideInInspector] public static SoundManager Instance;

    [Header("BGM option")]
    [SerializeField] private List<AudioClip> _BGM;
    [SerializeField] private float _changeDuration = 1f;

    [Header("0 Master, 1 BGM, 2 Effect, 3 UI")]
    [SerializeField] private List<AudioMixerGroup> _audioMixer;

    private AudioSource _BGMAudioSource;
    private ObjectPool _pools;
    public int BGMIndex { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _BGMAudioSource = GetComponent<AudioSource>();
            _BGMAudioSource.outputAudioMixerGroup = _audioMixer[1];
            _pools = GetComponent<ObjectPool>();
        }
    }

    public static void ChangeBGM(int index)
    {
        if (index >= Instance._BGM.Count || index < 0)
            return;

        Instance.StartCoroutine(Instance.SlowlyChangeSound(index));
        Instance.BGMIndex = index;
    }

    public static void ChangeVol(eSoundType type)
    {
        switch (type)
        {
            case eSoundType.Master:
                ChangeVol(eSoundType.BGM);
                break;
            case eSoundType.BGM:
                Instance._BGMAudioSource.volume = GameManager.Data.BGMVolume * GameManager.Data.MasterVolume;
                break;
        }
    }

    private IEnumerator SlowlyChangeSound(int index)
    {
        float time = 0.0f;
        float baseVolume = _BGMAudioSource.volume;
        while (time <= _changeDuration / 2f)
        {
            time += Time.deltaTime;
            _BGMAudioSource.volume = baseVolume * Mathf.Max(1 - time / _changeDuration, 0.0f);
            yield return null;
        }
        _BGMAudioSource.Stop();
        time = 0.0f;
        _BGMAudioSource.clip = _BGM[index];
        _BGMAudioSource.Play();
        while (time <= _changeDuration / 2f)
        {
            time += Time.deltaTime;
            _BGMAudioSource.volume = GameManager.Data.BGMVolume * GameManager.Data.MasterVolume * Mathf.Min(time / _changeDuration, 1.0f);
            yield return null;
        }
        _BGMAudioSource.volume = GameManager.Data.BGMVolume * GameManager.Data.MasterVolume;
        yield break;
    }

    /// <summary>
    /// ������ ����� �׷��� ȿ������ ���带 ����ϴ� ���
    /// </summary>
    /// <param name="type">����� �ͼ� �׷�</param>
    /// <param name="clip">����� ����� ����</param>
    /// <param name="minPitch">���� �� ������ ����</param>
    /// <param name="maxPitch">���� �� ������ ����</param>
    public static void PlayClip(eSoundType type, AudioClip clip, Vector3 worldPosition, float minPitch = 1.0f, float maxPitch = 1.0f)
    {
        GameObject obj = Instance._pools.SpawnFromPool(ePoolType.SoundSource);
        obj.SetActive(true);
        obj.transform.localPosition = obj.transform.worldToLocalMatrix * worldPosition;
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        float volume;
        switch (type)
        {
            case eSoundType.Master:
                volume = GameManager.Data.MasterVolume;
                break;
            case eSoundType.BGM:
                volume = GameManager.Data.BGMVolume * GameManager.Data.MasterVolume;
                break;
            case eSoundType.Effect:
                volume = GameManager.Data.EffectVolume * GameManager.Data.MasterVolume;
                break;
            case eSoundType.UI:
                volume = GameManager.Data.UIVolume * GameManager.Data.MasterVolume;
                break;
            default:
                volume = GameManager.Data.MasterVolume;
                break;
        }
        soundSource.Play(Instance._audioMixer[(int)type], clip, volume, minPitch, maxPitch);
    }
}
