using System;
using System.Collections;
using UnityEngine;

namespace Objects.Blocks
{
    public abstract class AbstractBlock : MonoBehaviour
    {
        protected bool _isHit;
        private string _blockAnimState;
        
        protected Animator _animator;
        protected BoxCollider _boxCollider;

        protected abstract bool CheckHit();
        protected abstract IEnumerator OnHit();

        private void Start()
        {
            _isHit = false;
            _blockAnimState = "_idle";
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            _isHit = CheckHit();
            
            if (_isHit)
            {
                Debug.Log("Block Hit");
                _blockAnimState = "_hit";
                StartCoroutine(OnHit());
            }
        }

        private void LateUpdate()
        {
            _animator.Play("block" + _blockAnimState);
        }
    }
}