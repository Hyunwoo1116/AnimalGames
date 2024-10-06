using MoewMerge.Animals.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoewMerge.Managers.Interfaces
{
    public interface ICatManager
    {
        bool OnLevelUpCat(CatCreateModel createCat);

        void AddCreateQueue(CatCreateModel createModel);
    }

}
