using MoewMerge.Cats.Model;
using MoewMerge.GameModel;
using MoewMerge.Managers.Interfaces;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace MoewMerge.Managers
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        public MoewParameter gameDatas;
        public static GameManager Instance
        {
            get
            {
                return instance;
            }
        }

        private int gameScore;

        private static GameManager instance = null;


        [SerializeField]
        private TextMeshProUGUI gameScoreUI;

        [Header("Managers")]
        public SoundManager SoundManager;
        public CatManager CatManager;

        private float topPosition = float.MinValue;
        public void Awake()
        {
            if (null == instance)
            {
                //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
                instance = this;

                //씬 전환이 되더라도 파괴되지 않게 한다.
                //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
                //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

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
                Debug.Log(gameDatas.BackgroundSound);
                SaveGameData();
            }
            else
            {
                gameDatas = new MoewParameter();
                SaveGameData();
            }
        }

        private void SaveGameData()
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
            gameScore = 0;
            CatManager.OnGameStart();
            //SoundManager.PlayBackgroundSound();
        }

        public void AddGameScore(CatLevel instanceCatLevel)
        {
            gameScore += ((int)instanceCatLevel + 1);
            updateGameScoreUI();
        }

        private void updateGameScoreUI()
        {
            gameScoreUI.text = gameScore.ToString();
        }

        public void OnGameEnd()
        {

        }

        public float GetLeftEndPosition(Vector2 endObjectPosition)
        {

            return Camera.main.ScreenToWorldPoint(Vector2.zero + endObjectPosition).x;

        }

        public float GetRightEndPosition(Vector2 endObjectPosition)
        {
            return Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height) - endObjectPosition).x;

        }

        public float GetTopPosition()
        {
            return topPosition = topPosition.Equals(float.MinValue) ? Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height) - new Vector2(0f, 471)).y : topPosition;
        }
    }

}
