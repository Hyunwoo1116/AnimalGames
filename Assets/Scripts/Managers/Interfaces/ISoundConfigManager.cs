namespace MoewMerge.Managers.Interfaces
{
    public interface ISoundConfigManager 
    {
        public bool GetSoundConfig(SoundConfigType soundConfigType);
        public void SetSoundConfig(SoundConfigType soundConfigType, bool enabled);
        public bool SaveSoundConfig();
    }

    public enum SoundConfigType
    {
        Effect,
        Background,
        Vibrate,
    }
}