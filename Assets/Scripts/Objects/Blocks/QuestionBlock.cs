using System.Collections;
using UnityEngine;

namespace Objects.Blocks
{
    public class QuestionBlock : AbstractBlock
    {
        protected override bool CheckHit()
        {
            //Debug.DrawRay(transform.position, Vector3.down, Color.magenta);
            //return Physics.Raycast(transform.position,Vector3.down, 15f, LayerMask.NameToLayer("Player"));

            RaycastHit hit;

            return Physics.SphereCast(transform.position, _boxCollider.size.y / 4, Vector3.down, out hit, 1, 1 << LayerMask.NameToLayer("Player"));
        }
        protected override IEnumerator OnHit()
        {
            Debug.Log("Question Block hit!");
            _animator.Play("block_hit");

            yield return new WaitForSeconds(1);

            _isHit = false;
        }
    }
}