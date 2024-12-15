using MoewMerge.Animals;
using MoewMerge.Managers.Interfaces;
using UnityEngine;
using Zenject;

public class EndCollider : MonoBehaviour
{
    [Inject] public IGameManager GameManager;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        CatMerge catMerge = collision.gameObject.GetComponent<CatMerge>();

        if (catMerge.IsActive)
        {
            if ((catMerge.transform.position.y - gameObject.transform.position.y) > 0)
                GameManager.OnGameEnd();
        }
        else
        {
            if ((catMerge.transform.position.y - gameObject.transform.position.y) <= 0)
                catMerge.IsActive = true;
        }
    }
}
