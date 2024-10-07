using PlasticGui.WorkspaceWindow;
using UnityEngine;

namespace MoewMerge.Managers.Interfaces
{
    public interface ISoundConfigManager 
    {
        public bool GetSoundConfig(SoundConfigType soundConfigType);
        public bool SetSoundConfig(SoundConfigType soundConfigType, bool enabled);
        public bool SaveSoundConfig();
    }

    public enum SoundConfigType
    {
        Effect,
        Background,
        Vibrate,
    }
}