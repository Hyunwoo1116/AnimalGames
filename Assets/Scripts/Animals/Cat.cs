using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{

    private Rigidbody2D catCollider;
    
    public Rigidbody2D RigidBody => catCollider ??= this.GetComponent<Rigidbody2D>();

    private CatMerge catMerge;

    public CatMerge CatMerge => catMerge ??= this.GetComponent<CatMerge>();

    private RectTransform rectTransform;
    public RectTransform RectTransform => rectTransform ??= this.GetComponent<RectTransform>();

    public float OriginScale;
    float ReadyScale = 30f;


    private IGameManager GameManager;
    private ISoundManager SoundManager; 


    public GameObject CatGauidLine;

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
            
            // ���� �ʿ� �������� ����� �ʰ� �����ؾߴ�.
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePosition.x);
            Debug.Log(RectTransform.sizeDelta);
            Debug.Log(transform.localScale);
            float leftBorder = GameManager.GetLeftEndPosition(Vector2.one * RectTransform.sizeDelta * transform.localScale);
            float rightBorder = GameManager.GetRightEndPosition(Vector2.one * RectTransform.sizeDelta * transform.localScale);
            
            if (mousePosition.x < leftBorder)
            {
                mousePosition.x = leftBorder;
            }
            else if (mousePosition.x > rightBorder)
            {
                mousePosition.x = rightBorder;
            }

            Vector3 nextPosition = Vector3.Lerp(transform.position, mousePosition, 0.5f);
            nextPosition.z = 0f;
            transform.position = nextPosition;
            RectTransform.anchoredPosition3D = new Vector3(RectTransform.anchoredPosition3D.x, RectTransform.anchoredPosition3D.y, RectTransform.anchoredPosition3D.z);
            RigidBody.simulated = false;
        }

        if( Input.GetMouseButtonUp(0) && !RigidBody.simulated)
        {
            CatGauidLine.SetActive(false);
            RigidBody.simulated = true;
            await SoundManager.PlayInstanceSound(); 
            GameManager.NextCats();
            GameManager.AddGameScore(CatMerge.CatLevel);
            this.enabled = false;
        }
    }

    public void Ready()
    {
        transform.localScale = Vector3.one * ReadyScale;
    }

    public void CatStart()
    {
        transform.position = new Vector3(0f, 8.7f, 0f);
        transform.localScale = Vector3.one * OriginScale;
        CatGauidLine.transform.localScale = Vector2.one * MoewMergeConst.CatCauidCanvasScale * MoewMergeConst.CatGauidDefaultScale / OriginScale;
        CatGauidLine.SetActive(true);
    }
}
