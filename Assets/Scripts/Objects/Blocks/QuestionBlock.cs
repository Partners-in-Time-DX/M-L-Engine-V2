using System.Collections;
using UnityEngine;

namespace Objects.Blocks
{
    public class QuestionBlock : AbstractBlock
    {
        protected override bool CheckHit()
        {
            return Physics.SphereCast(transform.position, _boxCollider.size.y / 4, Vector3.down, out _, 1, 1 << LayerMask.NameToLayer("Player"));
        }
        protected override IEnumerator OnHit()
        {
            Debug.Log("Question Block hit!");
            _animator.Play("block_hit");
            _isHit = false;

            yield return null;
        }
    }
}