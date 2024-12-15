using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using MoewMerge.Cats.Model;
using MoewMerge.Managers.Interfaces;
using MoewMerge.Animals.Models;

public class CatMerge : MonoBehaviour
{
    [field:SerializeField]
    public CatLevel CatLevel { get; set; }

    private ICatManager CatManager { get; set; }
    private IGameManager GameManager { get; set; }

    public bool IsActive = false;
    private bool IsMerge = false;


    #region Animation

    private Rigidbody2D rigidybody;
    public Rigidbody2D Rigidbody => rigidybody ??= this.GetComponent<Rigidbody2D>();

    private Animator animator;
    public Animator Animator => animator ??= this.GetComponent<Animator>();

    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.IsPlaying())
            return;
        if (collision.transform.TryGetComponent(out CatMerge targetCat))
        {
            if (CatLevel.Equals(targetCat.CatLevel) && !IsMerge)
            {
                IsMerge = true;
                Vector3 centerPosition = Vector3.Lerp(this.transform.position, targetCat.transform.position, 0.5f);
                CatManager.AddCreateQueue(
                    new CatCreateModel(gameObject, targetCat.gameObject, centerPosition, CatLevel.GetMoveNext())
                    );
                gameObject.SetActive(false);
            }
            IsActive = true;
        }
    }

    private void Update()
    {
        if (!GameManager.IsPlaying())
            return;
        Animator.SetFloat("Move", Rigidbody.linearVelocity.magnitude);
    }
    public void SetDependency(IGameManager GameManager, ICatManager CatManager)
    {
        this.GameManager = GameManager;
        this.CatManager = CatManager;
    }
}
