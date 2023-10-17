using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource;

    public void Play(AudioMixerGroup type, AudioClip clip, float volume, float minPitch, float maxPitch)
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        CancelInvoke();
        _audioSource.outputAudioMixerGroup = type;
        _audioSource.volume = volume;
        _audioSource.clip = clip;
        _audioSource.pitch = Random.Range(minPitch, maxPitch);
        _audioSource.Play();

        Invoke("Disable", clip.length + 1);
    }

    public void Disable()
    {
        _audioSource.Stop();
        gameObject.SetActive(false);
    }
}
