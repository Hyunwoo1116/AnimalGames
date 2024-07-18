using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMerge : MonoBehaviour
{
    public CatLevel CatLevel { get; set; }
    public ICatManager CatManager { get; set; }

    public bool IsActive = false;

    // Start is called before the first frame update
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out CatMerge targetCat))
        {
            Debug.Log($"Merge!!!! {targetCat.CatLevel}");
            if (CatLevel.Equals(targetCat.CatLevel))
            {
                Vector3 centerPosition = Vector3.Lerp(this.transform.position, targetCat.transform.position, 0.5f);
                Debug.Log($"centerPosition{centerPosition}");
                CatManager.AddCreateQueue(
                    new CatCreateModel(gameObject, targetCat.gameObject, centerPosition, CatLevel.GetMoveNext())
                    );
                Destroy(targetCat.gameObject);
                Destroy(gameObject);
            }
            IsActive = true;
        }
    }
}
