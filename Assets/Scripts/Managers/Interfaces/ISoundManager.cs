using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MoewMerge.Managers.Interfaces
{
    public interface ISoundManager
    {
        public Task PlayInstanceSound();
        public void UpdateSoundSetting();
        public bool EnableVibrate();
    }
}

