using MoewMerge.Managers;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    [SerializeField] private Image targetIcon;
    [SerializeField] private Toggle toggle;

    [SerializeField] private Sprite activeImage;
    [SerializeField] private Sprite unActiveImage;

    [SerializeField] private SoundManager SoundManager;

    [field : SerializeField]
    [SerializeField] SoundType Type { get; set; }
    public void OnValueChanged(bool isOn)
    {
        targetIcon.sprite = isOn ? activeImage : unActiveImage;
        SoundManager.SetAudioMute(Type, isOn);
    }
    public void Start()
    {
        SoundManager = FindObjectOfType<SoundManager>();
    }
}
