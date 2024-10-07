using MoewMerge.Managers.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MoewMerge.UI.Controller.SoundConfig
{
    public class SoundConfigControl : MonoBehaviour
    {
        [SerializeField]
        private Texture2D enabledTexture;
        [SerializeField]
        private Texture2D disableTexture;

        private RawImage rawImage;
        public RawImage TargetRawImage => rawImage ??= this.GetComponent<RawImage>();

        [Inject]
        private ISoundConfigManager SoundConfigManager { get; set; }

        [SerializeField]
        private SoundConfigType SoundConfigType;
        private void OnEnable()
        {
            TargetRawImage.texture = SoundConfigManager.GetSoundConfig(SoundConfigType) ? enabledTexture : disableTexture;
        }
    }
}
