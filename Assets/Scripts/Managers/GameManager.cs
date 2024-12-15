using MoewMerge.Cats.Model;
using MoewMerge.GameModel;
using MoewMerge.Localization.Model;
using MoewMerge.Managers.Interfaces;
using MoewMerge.UI.Canvas.Interfaces;
using MoewMerge.UI.Controller.GameEnd.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MoewMerge.Managers
{
    public class GameManager : MonoBehaviour, IGameManager, ISoundConfigManager
    {
        private bool isPlaying = true;
        public MoewParameter gameDatas;

        private int gameScore;
        public int GameScore
        {
            private set
            {
                gameScore = value;
                updateGameScoreUI();

            }
            get
            {
                return gameScore;
            }
        }
        [SerializeField]
        private TextMeshProUGUI gameScoreUI;
        [Header("Managers")]
        public SoundManager SoundManager;
        public CatManager CatManager;
        [field : SerializeField]
        private RectTransform catStartRectTransform;
        private float topPosition = float.MinValue;
        private void Start()
        {
#if UNITY_IOS || UNITY_ANDROID
            Application.targetFrameRate = 60;
#else
             QualitySettings.vSyncCount = 1;
#endif
            LoadOrCreateGameData();
            SoundManager.UpdateSoundSetting();
            OnGameStart();
        }

        [Inject] public IGameCanvas GameCanvas;
        [Inject] public IScreenCaptureManager ScreenCaptureManager;
        [Inject] public IGameEndController GameEndController;
        [Inject] public ILanguageManager LanguageManager { get; set; }

        public bool GetEffectSoundEnabled() => gameDatas.EffectSound;
        public bool GetBackgroundSoundEnabled() => gameDatas.BackgroundSound;
        public bool GetVibrateEnabled() => gameDatas.Vibrate;

        public void SetEffectSoundEnabled(bool enabled) => gameDatas.EffectSound = enabled;
        public void SetBackgroundSoundEnabled(bool enabled) => gameDatas.BackgroundSound = enabled;
        public void SetVibrateEnabled(bool enabled) => gameDatas.Vibrate = enabled;

        private void LoadOrCreateGameData()
        {
            string dataFilePath = MoewMergeConst.MoewGameDataFile;
            Debug.Log(dataFilePath);
            if (File.Exists(dataFilePath))
            {
                string fileDatas = File.ReadAllText(dataFilePath);
                Debug.Log(fileDatas);
                gameDatas = JsonConvert.DeserializeObject<MoewParameter>(fileDatas);
                SaveGameData();
            }
            else
            {
                gameDatas = new MoewParameter();
                SaveGameData();
            }
            LanguageManager.SetLocale(gameDatas.AppLocale);
        }

        public Locales GetLocale() => gameDatas.AppLocale;
        public Locales SetLocale(Locales locale) => gameDatas.AppLocale = locale;

        public void SaveGameData()
        {
            string datas = JsonConvert.SerializeObject(gameDatas);
            Debug.Log($"SaveDatas{datas}");
            File.WriteAllText(MoewMergeConst.MoewGameDataFile, datas);
        }
        public void NextCats()
        {
            CatManager.OnNextCat();
        }

        public int GetGameScore() => gameScore;

        public void OnGameStart()
        {
            GameScore = 0;
            CatManager.OnGameStart();
            SoundManager.PlayBackgroundSound();
            Time.timeScale = 1f;
        }

        public void AddGameScore(CatLevel instanceCatLevel)
        {
            GameScore += ((int)instanceCatLevel + 1);
        }

        private void updateGameScoreUI()
        {
            gameScoreUI.text = gameScore.ToString();
        }

        public async void OnGameEnd()
        {
            isPlaying = false;
            Time.timeScale = 0f;
            Texture2D texture = await ScreenCaptureManager.GetScreenTexture();
            GameEndController.SetResultTexture(texture);
            GameEndController.Show();
            Debug.Log("OnGameEnd");
        }

        public float GetLeftEndPosition(Vector2 objectSize)
        {
            return Camera.main.ScreenToWorldPoint(objectSize * GameCanvas.GetScaleFactor()).x;
        }

        public float GetRightEndPosition(Vector2 objectSize)
        {
            Vector2 ScreenSize = new Vector2(Screen.width, Screen.height);
            return Camera.main.ScreenToWorldPoint(ScreenSize - objectSize * GameCanvas.GetScaleFactor()).x;

        }
        public void ReStartGame()
        {
            SceneManager.LoadScene(0);
        }
        public float GetTopPosition()
        {
            if ( topPosition.Equals(float.MinValue))
            {
                Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, catStartRectTransform.position);
                float StartPosition = Camera.main.ScreenToWorldPoint(screenPos).y;
                topPosition = StartPosition;
                return topPosition;
            }
            else
            {
                return topPosition;
            }
        }
        public bool IsPlaying() => isPlaying;


        #region SoundDatas Setup
        public bool GetSoundConfig(SoundConfigType soundConfigType)
        {
            switch (soundConfigType)
            {
                case SoundConfigType.Effect:
                    return GetEffectSoundEnabled();
                case SoundConfigType.Background:
                    return GetBackgroundSoundEnabled();
                case SoundConfigType.Vibrate:
                    return GetVibrateEnabled();
            }
            return false;
        }

        public void SetSoundConfig(SoundConfigType soundConfigType, bool enabled)
        {
            switch (soundConfigType)
            {
                case SoundConfigType.Effect:
                    SetEffectSoundEnabled(enabled);
                    break;
                case SoundConfigType.Background:
                    SetBackgroundSoundEnabled(enabled);
                    break;
                case SoundConfigType.Vibrate:
                    SetVibrateEnabled(enabled);
                    break;
            }
            SoundManager.UpdateSoundSetting();
        }

        public bool SaveSoundConfig()
        {
            try
            {
                SaveGameData();
                return true;
            }
            catch (Exception error)
            {
                Debug.LogError(error.Message);
                return false;
            }
        }
        #endregion
    }

}
