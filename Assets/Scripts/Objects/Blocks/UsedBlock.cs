using System.Collections;
using UnityEngine;

namespace Objects.Blocks
{
    public class UsedBlock : AbstractBlock
    {
        protected override bool CheckHit()
        {
            return Physics.SphereCast(transform.position, _boxCollider.size.y / 4, Vector3.down, out _, 1, 1 << LayerMask.NameToLayer("Player"));
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