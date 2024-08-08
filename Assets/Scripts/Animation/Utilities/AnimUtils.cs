using System.Collections;
using UnityEngine;

namespace Animation.Utilities
{
    public class AnimUtils
    {
        private Animator _animator;

        public AnimUtils(Animator animator)
        {
            _animator = animator;
        }

        public IEnumerator WaitForAnimationToFinish()
        {
            float counter = 0;
            float time = GetAnimationTime();

            while (counter < time)
            {
                counter += Time.deltaTime;
                yield return null;
            }
        }
        
        private float GetAnimationTime()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).length;
        }
    }
}