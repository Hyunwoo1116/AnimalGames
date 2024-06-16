using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    
    public Rigidbody2D RigidBody => rigidbody ??= this.GetComponent<Rigidbody2D>();


    public float OriginScale;
    float ReadyScale = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePosition);
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

        if( Input.GetMouseButtonUp(0))
        {
            RigidBody.simulated = true;
            GameManager.Instance.NextCats();
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
