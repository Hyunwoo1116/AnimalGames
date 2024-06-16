using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{

    public List<Cat> Cats = new List<Cat>();

    // Start is called before the first frame update

    public Transform NextCatTransform;
    public Transform GameArea;

    public Cat CurrentCat;
    public Cat NextCat;
    
    private Cat GetRandomCats()
    {
        int RandomIndex = Random.Range(0, 5);

        Cat cat = Instantiate(Cats[RandomIndex]);
        return cat;
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

}
