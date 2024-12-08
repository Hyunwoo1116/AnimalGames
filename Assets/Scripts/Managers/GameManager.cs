using MoewMerge.Cats.Model;
using MoewMerge.GameModel;
using MoewMerge.Localization.Model;
using MoewMerge.Managers.Interfaces;
using MoewMerge.UI.Controller.GameEnd.Interfaces;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MoewMerge.Managers
{
    public class GameManager : MonoBehaviour, IGameManager
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

        private float topPosition = float.MinValue;
        private void Start()
        {
#if UNITY_IOS || UNITY_ANDROID
            Application.targetFrameRate = 60;
#else
             QualitySettings.vSyncCount = 1;
#endif
            LoadOrCreateGameData();
            OnGameStart();
        }

        [Inject] public IScreenCaptureManager ScreenCaptureManager;
        [Inject] public IGameEndController GameEndController;
        [Inject] public ILanguageManager LanguageManager { get; set; } 

        public bool GetEffectSoundEnabled() => gameDatas.EffectSound;
        public bool GetBackgroundSoundEnabled() => gameDatas.BackgroundSound;
        public bool GetVibrateEnabled() => gameDatas.Vibrate;
        public bool SetEffectSoundEnabled(bool enabled) => gameDatas.EffectSound = enabled;
        public bool SetBackgroundSoundEnabled(bool enabled) => gameDatas.BackgroundSound = enabled;
        public bool SetVibrateEnabled(bool enabled) => gameDatas.Vibrate = enabled;

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
            Time.timeScale = 1f;
            //SoundManager.PlayBackgroundSound();
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

        public float GetLeftEndPosition(Vector2 endObjectPosition)
        {
            return Camera.main.ScreenToWorldPoint(Vector2.zero + endObjectPosition).x;
        }

        public float GetRightEndPosition(Vector2 endObjectPosition)
        {
            return Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height) - endObjectPosition).x;

        }
        public void ReStartGame()
        {
            SceneManager.LoadScene(0);
        }
        public float GetTopPosition()
        {
            return topPosition = topPosition.Equals(float.MinValue) ? Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height) - new Vector2(0f, 471)).y : topPosition;
        }
        public bool IsPlaying() => isPlaying;
    }

}
