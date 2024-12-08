
using MoewMerge.Cats.Model;
using MoewMerge.Localization.Model;
using UnityEngine;

namespace MoewMerge.Managers.Interfaces
{
    public interface IGameManager
    {
        #region GameStatus
        public float GetLeftEndPosition(Vector2 endObjectPosition);
        public float GetRightEndPosition(Vector2 endObjectPosition);
        public void NextCats();
        public void AddGameScore(CatLevel instanceCatLevel);
        public float GetTopPosition();
        public void SaveGameData();
        public bool IsPlaying();
        #endregion
        #region Sound
        bool GetEffectSoundEnabled();
        bool GetBackgroundSoundEnabled();
        bool GetVibrateEnabled();
        bool SetEffectSoundEnabled(bool enable);
        bool SetBackgroundSoundEnabled(bool enable);
        bool SetVibrateEnabled(bool enable);
        #endregion
        #region Locale
        Locales SetLocale(Locales Locale);
        Locales GetLocale();
        #endregion

        public void ReStartGame();
        public void OnGameEnd();
    }
}

