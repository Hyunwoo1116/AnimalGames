using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatManager : MonoBehaviour, ICatManager
{

    public List<Cat> Cats = new List<Cat>();

    // Start is called before the first frame update

    public Transform NextCatTransform;
    public Transform GameArea;

    public Cat CurrentCat;
    public Cat NextCat;

    public Queue<CatCreateModel> catQueue = new Queue<CatCreateModel>();

    private Cat GetRandomCats()
    {
        int RandomIndex = Random.Range(0, 5);

        Cat cat = Instantiate(Cats[RandomIndex]);
        CatMerge catMerge = cat.GetComponent<CatMerge>();
        catMerge.CatLevel = (CatLevel) RandomIndex;
        catMerge.CatManager = this;
        return cat;
    }

    public void Update()
    {
        if(catQueue.Count > 0)
        {
            OnLevelUpCat(catQueue.Dequeue());
        }
    }

    public void OnGameStart()
    {
        Cat cat = GetRandomCats();

        CurrentCat = cat;
        CurrentCat.enabled = true;
        CurrentCat.transform.SetParent(GameArea, true);
        CurrentCat.CatStart();

        NextCat = GetRandomCats();
        NextCat.transform.SetParent(NextCatTransform, true);
        NextCat.transform.localPosition = Vector3.zero;
        NextCat.enabled = false;
        NextCat.Ready();

    }
    public void OnNextCat()
    {
        Cat cat = GetRandomCats();

        CurrentCat = NextCat;
        CurrentCat.enabled = true;
        CurrentCat.transform.SetParent(GameArea, true);
        CurrentCat.CatStart();
        NextCat = cat;

        NextCat.enabled = false;
        NextCat.transform.SetParent(NextCatTransform, true);
        NextCat.transform.localPosition = Vector3.zero;
        NextCat.Ready();
    }

    public bool OnLevelUpCat(CatCreateModel createModel) 
    {
        try
        {
            Cat cat = Instantiate(Cats[(int)createModel.catLevel], GameArea);
            cat.transform.position = createModel.createPosition;
            cat.RigidBody.simulated = true;
            cat.enabled = false;
            
            CatMerge catMerge = cat.GetComponent<CatMerge>();
            catMerge.CatManager = this;
            catMerge.CatLevel = createModel.catLevel;

            GameManager.Instance.AddGameScore(createModel.catLevel);
            cat.transform.localScale = Vector3.one * cat.OriginScale;
            
            return true;
        }
        catch (Exception error)
        {
            Debug.LogError(error.Message);
            return false;
        }
        
    }

    public void AddCreateQueue(CatCreateModel createModel)
    {
        CatCreateModel duplicateCheck = catQueue.FirstOrDefault(cat => cat.collisionObject.Equals(createModel.source) || cat.source.Equals(createModel));
        if (duplicateCheck.IsUnityNull())
            catQueue.Enqueue(createModel);
    }
}
