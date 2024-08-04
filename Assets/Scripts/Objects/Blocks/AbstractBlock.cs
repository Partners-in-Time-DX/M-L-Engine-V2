using System;
using System.Collections;
using UnityEngine;

namespace Objects.Blocks
{
    public abstract class AbstractBlock : MonoBehaviour
    {
        protected bool _isHit;
        
        protected Animator _animator;
        protected BoxCollider _boxCollider;

        protected abstract bool CheckHit();
        protected abstract IEnumerator OnHit();

        private void Start()
        {
            _isHit = false;
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void FixedUpdate()
        {
            _isHit = CheckHit();
            Debug.Log("Block is Hit: " + _isHit);
            
            if (_isHit)
            {
                Debug.Log("Block Hit");
                StartCoroutine(OnHit());
                Debug.Log("Finished OnHit");
            }
        }
    }
}