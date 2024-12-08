using MoewMerge.Animals.Models;
using MoewMerge.Managers.Interfaces;
using MoewMerge.UI.Controller.CatStep;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MoewMerge.Animals;
using Random = UnityEngine.Random;
using MoewMerge.Cats.Model;
using Zenject;

namespace MoewMerge.Managers
{
    public class CatManager : MonoBehaviour, ICatManager
    {
        [Inject] IGameManager GameManager;

        public List<Cat> Cats = new List<Cat>();
        public Transform NextCatTransform;
        public Transform GameArea;
        public Cat CurrentCat;
        public Cat NextCat;
        public Queue<CatCreateModel> catQueue = new Queue<CatCreateModel>();
        public SoundManager SoundManager;
        public CatStepController CatStepController;
        private Cat GetRandomCats()
        {
            int RandomIndex = Random.Range(0, 5);

            Cat cat = Instantiate(Cats[RandomIndex]);
            cat.SetDependency(GameManager, SoundManager);
            
            CatMerge catMerge = cat.GetComponent<CatMerge>();
            catMerge.CatLevel = (CatLevel)RandomIndex;
            catMerge.SetDependency(GameManager, this);

            return cat;
        }

        public void LateUpdate()
        {
            if (catQueue.Count > 0)
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
            NextCat.transform.localPosition = new Vector3(MoewMergeConst.NextCatXPosition, 0f, 0f);
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
            NextCat.transform.localPosition = new Vector3(MoewMergeConst.NextCatXPosition, 0f, 0f);
            NextCat.Ready();
        }

        public bool OnLevelUpCat(CatCreateModel createModel)
        {
            try
            {
                if (createModel.catLevel == 0)
                {
                    return false;
                }

                SoundManager.PlayMergeSound();
                CatStepController.PlayCatMerge(createModel.catLevel);
                Cat cat = Instantiate(Cats[(int)createModel.catLevel], GameArea);
                cat.SetDependency(GameManager, SoundManager);
                cat.transform.position = createModel.createPosition;
                cat.RigidBody.simulated = true;
                cat.enabled = false;

                CatMerge catMerge = cat.GetComponent<CatMerge>();
                catMerge.SetDependency(GameManager, this);
                catMerge.CatLevel = createModel.catLevel;

                GameManager.AddGameScore(createModel.catLevel);
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
            if (duplicateCheck is null)
                catQueue.Enqueue(createModel);
        }
    }

}
