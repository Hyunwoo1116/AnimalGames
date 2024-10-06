using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MoewMerge.Managers.Interfaces
{
    public interface ISoundManager
    {
        public void SetEffectVolume(float volume);
        public void SetBackgroundVolume(float volume);
        public float GetEffectVolume();
        public float GetBackgroundVolume();
        public Task PlayInstanceSound();

    }
}

