using UnityEngine;

namespace MoewMerge.UI.Controller.CatStep
{
    public class CatStepAnimator : MonoBehaviour
    {
        private Animator animator;

        public bool isPlaying;
        private int hash;
        public void SetTrigger(string triggerString)
        {
            animator.SetTrigger(triggerString);
        }

        public bool IsPlaying => isPlaying;

        // Start is called before the first frame update
        void Start()
        {
            animator = this.GetComponent<Animator>();
            hash = Animator.StringToHash("CatIdle");
        }

        // Update is called once per frame
        void Update()
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (isPlaying)
            {
                Debug.Log($"{info.shortNameHash} + : {gameObject.name}");
                
                if (info.shortNameHash.Equals(hash))
                    isPlaying = false;
            }
            else
            {
                if (!info.shortNameHash.Equals(hash))
                    isPlaying = true;
            }
        }
    }

}

