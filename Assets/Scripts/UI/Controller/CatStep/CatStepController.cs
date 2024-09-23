using MoewMerge.Cat.Model;
using MoewMerge.UI.Controller.CatStep.Interfaces;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace MoewMerge.UI.Controller.CatStep
{
    public class CatStepController : MonoBehaviour, ICatStepController
    {
        public List<Animator> StepAnimators = new List<Animator>();
        
        public void PlayCatMerge(CatLevel targetCat)
        {
            CatLevel beforeCat = targetCat.GetMoveBefore();
            int beforeAnimator = (int)beforeCat;
            int nextAnimator = (int)targetCat;
            StepAnimators[beforeAnimator].SetTrigger("Down");
            StepAnimators[nextAnimator].SetTrigger("Up");
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}