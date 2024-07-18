using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, ISoundManager
{
    [Header("Source")]
    [SerializeField] private AudioSource effectAudio;
    [SerializeField] private AudioSource backgroundAudio;

    [Header("Resource")]
    public AudioClip backgroundClip;
    public AudioClip effectClipInstance;
    public AudioClip effectClipMerge;
    [Header("Property")]
    public float effectSoundVolume;
    public float backgroundSoundVolume;

    public float GetBackgroundVolume() => backgroundSoundVolume;


    public float GetEffectVolume() => effectSoundVolume;


    public void SetBackgroundVolume(float volume)
    {
        backgroundSoundVolume = volume;
    }

    public void SetEffectVolume(float volume)
    {
        backgroundSoundVolume = volume;
    }

    private void Start()
    {
        backgroundSoundVolume = backgroundAudio.volume;
        effectSoundVolume = effectAudio.volume;
    }
}
