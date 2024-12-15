using MoewMerge.Collider.Enums;
using MoewMerge.UI.Canvas.Interfaces;
using UnityEngine;
using Zenject;

namespace MoewMerge.Collider
{
    public class ColliderResize : MonoBehaviour
    {
        public ColliderPosition Position;

        [Inject] public IGameCanvas GameCanvas;

        private RectTransform rectTransform;
        public RectTransform RectTransform => rectTransform ??= this.GetComponent<RectTransform>();
        private BoxCollider2D boxCollider;

        public BoxCollider2D BoxCollider => boxCollider ??= this.GetComponent<BoxCollider2D>();

        private void Start()
        {
            Debug.Log($"ScreenWidth : {Screen.width} , ScreenHeight {Screen.height}");
            Debug.Log($"this.Gameobject.name{gameObject.name}, RectTransform : {RectTransform.sizeDelta}");

            BoxCollider.size = CalcualteBoxCollider();
        }

        private Vector2 CalcualteBoxCollider()
        {
            switch (Position)
            {
                case ColliderPosition.Right:
                case ColliderPosition.Left:
                    return new Vector2(BoxCollider.size.x, GameCanvas.GetCanvsHieght());
                case ColliderPosition.Bottom:
                case ColliderPosition.Top:
                    return new Vector2(GameCanvas.GetCanvasWidth(), BoxCollider.size.y);
            }
            return Vector2.zero;
        }
    }
}
