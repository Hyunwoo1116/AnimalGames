using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    [SerializeField] private Image targetIcon;
    [SerializeField] private Toggle toggle;

    [SerializeField] private Sprite activeImage;
    [SerializeField] private Sprite unActiveImage;

    public void OnValueChanged(bool isOn)
    {
        targetIcon.sprite = isOn ? activeImage : unActiveImage;
    }
}
