using MoewMerge.Cat.Model;
using MoewMerge.UI.Controller.CatStep.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;

namespace MoewMerge.UI.Controller.CatStep
{
    public class CatStepController : MonoBehaviour, ICatStepController
    {
        public List<CatStepAnimator> StepAnimators = new List<CatStepAnimator>();
        
        public async void PlayCatMerge(CatLevel targetCat)
        {
            CatLevel beforeCat = targetCat.GetMoveBefore();
            int beforeAnimator = (int)beforeCat;
            int nextAnimator = (int)targetCat;
            CatStepAnimator bAnimator = StepAnimators[beforeAnimator];
            CatStepAnimator aAnimator = StepAnimators[nextAnimator];
            while (aAnimator.IsPlaying || bAnimator.IsPlaying)
                await Task.Delay(10);
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