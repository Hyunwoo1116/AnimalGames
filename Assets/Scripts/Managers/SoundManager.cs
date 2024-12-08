using MoewMerge.Managers.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;


namespace MoewMerge.Managers
{
    public class SoundManager : MonoBehaviour, ISoundManager
    {
        [Header("Source")]
        [SerializeField] private AudioSource effectAudio;
        [SerializeField] private AudioSource backgroundAudio;

        private bool enableVibrate;
        [Header("Resource")]
        public AudioClip backgroundClip;
        public AudioClip effectClipInstance;
        public AudioClip effectClipMerge;
        [Inject] public IGameManager GameManager;

        public void PlayMergeSound()
        {
            effectAudio.PlayOneShot(effectClipMerge);
        }
        public async Task PlayInstanceSound()
        {
            effectAudio.PlayOneShot(effectClipInstance);

            await WaitSoundLength();
        }


        public void SetAudioMute(SoundType type, bool isPlay)
        {
            switch (type)
            {
                case SoundType.None:
                    break;
                case SoundType.Effect:
                    effectAudio.mute = !isPlay;
                    break;
                case SoundType.Background:
                    backgroundAudio.mute = !isPlay;
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
            float clipLength = effectClipInstance.length * 3f;
            float delayTime = 0f;
            while (delayTime <= clipLength)
            {
                delayTime += Time.deltaTime;
                await Task.Yield();
            }
        }
        public void UpdateSoundSetting()
        {
            backgroundAudio.mute = !GameManager.GetBackgroundSoundEnabled();
            effectAudio.mute = !GameManager.GetEffectSoundEnabled();
            enableVibrate = GameManager.GetVibrateEnabled();
        }
    }
    public enum SoundType
    {
        None,
        Effect,
        Background,
    }

}

