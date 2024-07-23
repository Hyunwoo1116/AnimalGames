using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class CatMerge : MonoBehaviour
{
    [field:SerializeField]
    public CatLevel CatLevel { get; set; }
    public ICatManager CatManager { get; set; }

    public bool IsActive = false;
    private bool IsMerge = false;
    // Start is called before the first frame update
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out CatMerge targetCat))
        {
            if (CatLevel.Equals(targetCat.CatLevel) && !IsMerge)
            {
                IsMerge = true;
                Vector3 centerPosition = Vector3.Lerp(this.transform.position, targetCat.transform.position, 0.5f);
                Debug.Log($"centerPosition{centerPosition}");
                if (CatLevel == CatLevel.Level11)
                    return;
                CatManager.AddCreateQueue(
                    new CatCreateModel(gameObject, targetCat.gameObject, centerPosition, CatLevel.GetMoveNext())
                    );
                
                Destroy(gameObject);
            }
            IsActive = true;
        }
    }
}
