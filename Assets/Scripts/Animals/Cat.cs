using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{

    private Rigidbody2D catCollider;
    
    public Rigidbody2D RigidBody => catCollider ??= this.GetComponent<Rigidbody2D>();

    private CatMerge catMerge;

    public CatMerge CatMerge => catMerge ??= this.GetComponent<CatMerge>();

    public float OriginScale;
    float ReadyScale = 10f;
    public SoundManager SoundManager => soundManager ??= FindObjectOfType<SoundManager>();

    private SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    async void Update()
    {
        if (Input.GetMouseButton(0) && !RigidBody.simulated)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float leftBorder = -4.5f;
            float rightBorder = 4.5f;
            mousePosition.z = 0f;
            mousePosition.y = 8.7f;
            if ( mousePosition.x < leftBorder)
            {
                mousePosition.x = leftBorder;
            } else if (mousePosition.x > rightBorder)
            {
                mousePosition.x = rightBorder;
            }

            Vector3 nextPosition = Vector3.Lerp(transform.position, mousePosition, 0.5f);
            transform.position = nextPosition;
            RigidBody.simulated = false;
        }

        if( Input.GetMouseButtonUp(0) && !RigidBody.simulated)
        {
            RigidBody.simulated = true;
            await SoundManager.PlayInstanceSound();
            Debug.Log("SOundEnd");
            GameManager.Instance.NextCats();
            GameManager.Instance.AddGameScore(CatMerge.CatLevel);
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

    }
}
