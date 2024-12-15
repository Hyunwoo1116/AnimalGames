using MoewMerge.UI.Canvas.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace MoewMerge.UI.Canvas
{
    public class GameCanvas : MonoBehaviour, IGameCanvas
    {
        private CanvasScaler scaler;
        public CanvasScaler Scaler => scaler ??= this.GetComponent<CanvasScaler>();
        private RectTransform rectTransform;
        public RectTransform RectTransform => rectTransform ??= this.GetComponent<RectTransform>();
        public float GetCanvasWidth() => RectTransform.sizeDelta.x;
        public float GetCanvsHieght() => RectTransform.sizeDelta.y;

        public float GetScaleFactor()
        {
            float widthScale = Screen.width / Scaler.referenceResolution.x;
            float heightScale = Screen.height / Scaler.referenceResolution.y;
            float scaleFactor = Mathf.Pow(widthScale, 1 - Scaler.matchWidthOrHeight) * Mathf.Pow(heightScale, Scaler.matchWidthOrHeight);
            
            return scaleFactor;
        }
    }
}