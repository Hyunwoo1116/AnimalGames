using MoewMerge.Managers.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MoewMerge.Animals
{
    public class Cat : MonoBehaviour
    {

        private Rigidbody2D catCollider;

        public Rigidbody2D RigidBody => catCollider ??= this.GetComponent<Rigidbody2D>();

        private CatMerge catMerge;

        public CatMerge CatMerge => catMerge ??= this.GetComponent<CatMerge>();

        private RectTransform rectTransform;
        public RectTransform RectTransform => rectTransform ??= this.GetComponent<RectTransform>();

        public float OriginScale;
        private float ReadyScale = 30f;
        private float minBorderX = float.MinValue;
        private float maxBorderX = float.MinValue;

        private IGameManager GameManager;
        private ISoundManager SoundManager;
        public GameObject CatGauidLine;
        public GameObject CatOutline;

        // Start is called before the first frame update
        void Start()
        {

        }

        public void SetDependency(IGameManager GameManager, ISoundManager SoundManager)
        {
            this.GameManager = GameManager;
            this.SoundManager = SoundManager;
        }
        // Update is called once per frame
        async void Update()
        {
            if (Input.GetMouseButton(0) && !RigidBody.simulated)
            {

                // 수정 필요 범위에서 벗어나지 않게 설정해야댐.
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log(mousePosition.x);
                Debug.Log(RectTransform.sizeDelta);
                Debug.Log(transform.localScale);
                minBorderX = minBorderX.Equals(float.MinValue) ? GameManager.GetLeftEndPosition(Vector2.one * RectTransform.sizeDelta * transform.localScale / 2f) : minBorderX;
                maxBorderX = maxBorderX.Equals(float.MinValue) ? GameManager.GetRightEndPosition(Vector2.one * RectTransform.sizeDelta * transform.localScale / 2f) : maxBorderX;

                mousePosition.x = Mathf.Clamp(mousePosition.x, minBorderX, maxBorderX);

                mousePosition.y = GameManager.GetTopPosition();
                Vector3 nextPosition = Vector3.Lerp(transform.position, mousePosition, 0.5f);
                nextPosition.z = 0f;
                transform.position = nextPosition;
                RectTransform.anchoredPosition3D = new Vector3(RectTransform.anchoredPosition3D.x, RectTransform.anchoredPosition3D.y, RectTransform.anchoredPosition3D.z);
                RigidBody.simulated = false;
            }

            if (Input.GetMouseButtonUp(0) && !RigidBody.simulated)
            {
                DropDownCat();
            }
        }

        private async void DropDownCat()
        {
            CatGauidLine.SetActive(false);
            RigidBody.simulated = true;
            await SoundManager.PlayInstanceSound();
            GameManager.NextCats();
            GameManager.AddGameScore(CatMerge.CatLevel);
            this.enabled = false;
        }
        // 넥스트 UI
        public void Ready()
        {
            transform.localScale = Vector3.one * ReadyScale;
            CatOutline.gameObject.SetActive(true);
        }

        // 현재 시작 된 Cat

        public void CatStart()
        {
            transform.position = new Vector3(0f, GameManager.GetTopPosition(), 0f);
            transform.localScale = Vector3.one * OriginScale;
            CatGauidLine.transform.localScale = Vector2.one * MoewMergeConst.CatCauidCanvasScale * MoewMergeConst.CatGauidDefaultScale / OriginScale;
            CatGauidLine.SetActive(true);
            CatOutline.gameObject.SetActive(false);
        }
    }

}
