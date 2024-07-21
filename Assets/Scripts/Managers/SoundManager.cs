using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
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

    public void PlayMergeSound()
    {
        effectAudio.PlayOneShot(effectClipMerge);
    }
    public async Task PlayInstanceSound()
    {
        effectAudio.PlayOneShot(effectClipInstance);

        await WaitSoundLength();
    }


    public void SetAudioMute(SoundType type, bool isMute)
    {
        switch (type)
        {
            case SoundType.None:
                break;
            case SoundType.Effect:
                effectAudio.mute = !isMute;
                break;
            case SoundType.Background:
                backgroundAudio.mute = !isMute;
                break;
        }
    }




    public void PlayBackgroundSound()
    {
        backgroundAudio.clip = backgroundClip;
        backgroundAudio.Play();
    }
    private async Task WaitSoundLength()
    {
        float clipLength = effectClipInstance.length * 2;
        float delayTime = 0f;
        while (delayTime <= clipLength)
        {
            delayTime += 0.01f;
            await Task.Delay(10);
        }
    }


}
public enum SoundType
{
    None,
    Effect,
    Background,
}
