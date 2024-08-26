using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndCollider : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Enter2D{collision.gameObject.name}");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CatMerge catMerge = collision.gameObject.GetComponent<CatMerge>();

        if (catMerge.IsActive)
        {
            if ((catMerge.transform.position.y - gameObject.transform.position.y) > 0)
                GameManager.Instance.OnGameEnd();
        }
        else
        {
            if ((catMerge.transform.position.y - gameObject.transform.position.y) <= 0)
                catMerge.IsActive = true;
        }/*
        Debug.Log($"{(collision.gameObject.transform.position.y - gameObject.transform.position.y) > 0}" +
            $"\nStay2D{collision.gameObject.transform.position}\n" +
            $"Stay2D:Collider{gameObject.transform.position}");*/

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"Exit2D{collision.gameObject.name}");
    }
}
