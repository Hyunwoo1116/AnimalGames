using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Cat currentCat;

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButton(0))
        {
            Vector3 MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f);
            Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(MousePosition);
            Vector3 CatPosition = new Vector3(mouseToWorld.x, mouseToWorld.y, 0);
            currentCat.RigidBody.simulated = false;
            currentCat.transform.localPosition = CatPosition;
            
            Debug.Log($"MouseClicks{CatPosition}");

        }

        if (Input.GetMouseButtonUp(0))
        {
            currentCat.RigidBody.simulated = true;
        }*/
        //10´ç 0.2
    }
}
