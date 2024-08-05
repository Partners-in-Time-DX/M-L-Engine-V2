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
            return Physics.SphereCast(transform.position, _boxCollider.size.y / 4, Vector3.down, out _, 1, 1 << LayerMask.NameToLayer("Player"));
        }
        protected override IEnumerator OnHit()
        {
            Debug.Log("Question Block hit!");
            _animator.Play("block_hit");
            float counter = 0;
            float time = GetAnimationTime();

            while (counter < time)
            {
                counter += Time.deltaTime;
                yield return null;
            }
            
            _objectTransformer.TransformToObject();
            
            _isHit = false;
        }

        private float GetAnimationTime()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).length;
        }
    }
}