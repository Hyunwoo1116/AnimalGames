using MoewMerge.Managers;
using MoewMerge.Managers.Interfaces;
using MoewMerge.UI.Controller.GameEnd.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MoewMerge.UI.Controller.GameEnd
{
    public class GameEndController : MonoBehaviour, IGameEndController
    {
        [Inject] IGameManager GameManager;
        public RawImage ResultRawImage;
        public Button GameRetryButton;
        public Button SaveFileButton;
        
        private void Start()
        {
            GameRetryButton.onClick.AddListener(RestartGame);
            SaveFileButton.onClick.AddListener(SaveResultImage);
        }

        private void RestartGame()
        {
            GameManager.ReStartGame();
            Debug.Log("RESTART");
        }
        private void SaveResultImage()
        {

        }
        public void SetResultTexture(Texture2D texture)
        {
            ResultRawImage.texture = texture;
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
