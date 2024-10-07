using MoewMerge.Managers.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MoewMerge.UI.Controller.SoundConfig
{
    [RequireComponent(typeof(Button))]
    public class SoundConfigControl : MonoBehaviour
    {
        [SerializeField]
        private Texture2D enabledTexture;
        [SerializeField]
        private Texture2D disableTexture;

        private RawImage rawImage;
        public RawImage TargetRawImage => rawImage ??= this.GetComponent<RawImage>();

        private Button configButton;
        public Button ConfigButton => configButton ??= this.GetComponent<Button>();
        [Inject]
        private ISoundConfigManager SoundConfigManager { get; set; }

        [SerializeField]
        private SoundConfigType SoundConfigType;

        private bool isConfigEnabled;
        private void OnEnable()
        {
            isConfigEnabled = SoundConfigManager.GetSoundConfig(SoundConfigType);
            TargetRawImage.texture = isConfigEnabled ? enabledTexture : disableTexture;

            ConfigButton.onClick.RemoveAllListeners();
            ConfigButton.onClick.AddListener(ToggleConfig);
        }
        private void ToggleConfig()
        {
            isConfigEnabled = !isConfigEnabled;
            TargetRawImage.texture = isConfigEnabled ? enabledTexture : disableTexture;
            SoundConfigManager.SetSoundConfig(SoundConfigType, isConfigEnabled);
        }
        private void OnDisable()
        {
            SoundConfigManager.SaveSoundConfig();
        }



    }
}
