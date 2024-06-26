using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICatManager 
{
    bool OnLevelUpCat(CatCreateModel createCat);

    void AddCreateQueue(CatCreateModel createModel);
}
