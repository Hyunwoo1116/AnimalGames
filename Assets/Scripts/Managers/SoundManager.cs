using MoewMerge.Managers.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


namespace MoewMerge.Managers
{
    public class SoundManager : MonoBehaviour, ISoundManager, ISoundConfigManager
    {
        [Header("Source")]
        [SerializeField] private AudioSource effectAudio;
        [SerializeField] private AudioSource backgroundAudio;

        [Header("Resource")]
        public AudioClip backgroundClip;
        public AudioClip effectClipInstance;
        public AudioClip effectClipMerge;

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
            float clipLength = effectClipInstance.length * 1.1f;
            float delayTime = 0f;
            while (delayTime <= clipLength)
            {
                delayTime += 0.01f;
                await Task.Delay(10);
            }
        }
        public bool GetEffectSoundEnabled() => GameManager.Instance.GetEffectSoundEnabled();
        public bool GetBackgroundSoundEnabled() => GameManager.Instance.GetBackgroundSoundEnabled();
        public bool GetVibrateEnabled() => GameManager.Instance.GetVibrateEnabled();
        public bool SetEffectSoundEnabled(bool enabled) => GameManager.Instance.SetEffectSoundEnabled(enabled);
        public bool SetBackgroundSoundEnabled(bool enabled) => GameManager.Instance.SetBackgroundSoundEnabled(enabled);
        public bool SetVibrateEnabled(bool enabled) => GameManager.Instance.SetVibrateEnabled(enabled);

        public bool GetSoundConfig(SoundConfigType soundConfigType)
        {
            switch (soundConfigType)
            {
                case SoundConfigType.Effect:
                    return GameManager.Instance.GetEffectSoundEnabled();
                case SoundConfigType.Background:
                    return GameManager.Instance.GetBackgroundSoundEnabled();
                case SoundConfigType.Vibrate:
                    return GameManager.Instance.GetVibrateEnabled();
            }
            return false;
        }

        public bool SetSoundConfig(SoundConfigType soundConfigType, bool enabled)
        {
            switch (soundConfigType)
            {
                case SoundConfigType.Effect:
                    return SetEffectSoundEnabled(enabled);
                case SoundConfigType.Background:
                    return SetBackgroundSoundEnabled(enabled);
                case SoundConfigType.Vibrate:
                    return SetVibrateEnabled(enabled);
            }
            return false;
        }
    }
    public enum SoundType
    {
        None,
        Effect,
        Background,
    }

}

