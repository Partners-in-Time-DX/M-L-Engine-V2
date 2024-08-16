using System.Collections;
using UnityEngine;

namespace Objects.Blocks
{
    public class QuestionBlock : AbstractBlock
    {
        private ObjectTransformer _objectTransformer;

        private void Awake()
        {
            _objectTransformer = GetComponent<ObjectTransformer>();
        }
        protected override bool CheckHit()
        {
            return BlockRayCastPlayerHitCheck();
        }
        protected override IEnumerator OnHit()
        {
            Debug.Log("Question Block hit!");
            _animator.Play("block_hit");

            // Wait for block hit animation to finish before transforming into the used block
            yield return _animUtils.WaitForAnimationToFinish();
            
            _objectTransformer.TransformToObject();
            
            _isHit = false;
        }
    }
}