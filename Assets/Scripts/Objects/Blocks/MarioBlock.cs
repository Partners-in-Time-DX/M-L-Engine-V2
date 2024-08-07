using System.Collections;
using Objects.Blocks.Helpers;
using UnityEngine;

namespace Objects.Blocks
{
    public class MarioBlock : AbstractBlock
    {
        private ObjectTransformer _objectTransformer;
        private string _playerHitTag;

        private void Awake()
        {
            _objectTransformer = GetComponent<ObjectTransformer>();
        }
        protected override bool CheckHit()
        {
            if (Physics.SphereCast(transform.position, _boxCollider.size.y / 4, Vector3.down, out RaycastHit hit, 1, 1 << LayerMask.NameToLayer("Player")))
            {
                _playerHitTag = BlockHelper.GetPlayerTagFromBlockHit(hit);

                return true;
            }

            return false;
        }

        protected override IEnumerator OnHit()
        {
            Debug.Log("Question Block hit!");
            _animator.Play("block_hit");

            // Wait for block hit animation to finish before transforming into the used block
            yield return _animUtils.WaitForAnimationToFinish();

            if (_playerHitTag == "Mario")
            {
                _objectTransformer.TransformToObject();   
            }
            
            _isHit = false;
        }
    }
}