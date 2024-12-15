using MoewMerge.Collider.Enums;
using UnityEngine;

namespace MoewMerge.Collider
{
    public class ColliderResize : MonoBehaviour
    {
        public ColliderPosition Position;

        private BoxCollider2D boxCollider;

        public BoxCollider2D BoxCollider => boxCollider ??= this.GetComponent<BoxCollider2D>();

        private void Start()
        {
            Debug.Log($"ScreenWidth : {Screen.width} , ScreenHeight {Screen.height}");
            BoxCollider.size = CalcualteBoxCollider();
        }

        private Vector2 CalcualteBoxCollider()
        {
            switch (Position)
            {
                case ColliderPosition.Right:
                case ColliderPosition.Left:
                    return new Vector2(BoxCollider.size.x, Screen.height);
                case ColliderPosition.Bottom:
                case ColliderPosition.Top:
                    return new Vector2(Screen.width * 2f, BoxCollider.size.y);
            }
            return Vector2.zero;
        }
    }
}
