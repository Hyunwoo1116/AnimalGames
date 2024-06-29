using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private int gameScore;
    
    private static GameManager instance = null;


    [SerializeField] private TextMeshProUGUI gameScoreUI;

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
        CatManager.OnGameStart();
        OnGameStart();
    }


    public CatManager CatManager;

    public void NextCats()
    {

        CatManager.OnNextCat();
        
    }

    public int GetGameScore() => gameScore;

    private void OnGameStart()
    {
        gameScore = 0;
    }

    public void AddGameScore(CatLevel instanceCatLevel)
    {
        gameScore += ((int)instanceCatLevel + 1) * 10;
        updateGameScoreUI();
    }

    private void updateGameScoreUI()
    {
        gameScoreUI.text = "GameScore : " + gameScore.ToString();
    }


}
