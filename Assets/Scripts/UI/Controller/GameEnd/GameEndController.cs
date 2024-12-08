using MoewMerge.Managers;
using MoewMerge.Managers.Interfaces;
using MoewMerge.UI.Controller.GameEnd.Interfaces;
using System.IO;
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
            SaveFileButton.onClick.AddListener(ShareResult);
        }

        private void RestartGame()
        {
            GameManager.ReStartGame();
            Debug.Log("RESTART");
        }
        private void ShareResult()
        {
            Texture2D texture = (Texture2D) ResultRawImage.texture;

            byte[] array = texture.EncodeToPNG();
            string path = Path.Combine(Application.temporaryCachePath, "ShareImage.png");
            File.WriteAllBytes(path, array);
            new NativeShare().AddFile(path)
            .SetSubject("Subject goes here").SetText("Hello world!").SetUrl("https://github.com/yasirkula/UnityNativeShare")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
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
