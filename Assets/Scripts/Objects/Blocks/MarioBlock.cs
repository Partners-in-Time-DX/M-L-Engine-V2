using System.Collections;
using Objects.Blocks.Helpers;
using UnityEngine;

namespace Objects.Blocks
{
    public class MarioBlock : AbstractBlock
    {
        private ObjectTransformer _objectTransformer;
        private bool _playerHitMario;

        private void Awake()
        {
            _objectTransformer = GetComponent<ObjectTransformer>();
        }
        protected override bool CheckHit()
        {
            if (BlockRayCast())
            {
                _playerHitMario = BlockHelper.CheckPlayerTagFromBlockHit(_hit, "Mario");

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

            if (_playerHitMario)
            {
                _objectTransformer.TransformToObject();   
            }
            
            _isHit = false;
        }
    }
}