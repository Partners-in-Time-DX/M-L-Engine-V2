using System.Collections;
using UnityEngine;

namespace Objects.Blocks
{
    public class UsedBlock : AbstractBlock
    {
        protected override bool CheckHit()
        {
            return BlockRayCastPlayerHitCheck();
        }

        protected override IEnumerator OnHit()
        {
            Debug.Log("Used Block hit!");
            _animator.Play("block_hit");

            yield return _animUtils.WaitForAnimationToFinish();
            
            _isHit = false;

            yield return null;
        }
    }
}