using System;
using UnityEngine;

namespace Objects
{
    public class ObjectTransformer : MonoBehaviour
    {
        [SerializeField] private GameObject _newObject;
        [SerializeField] private LayerMask _layer;

        public void TransformToObject()
        {
            // Store the current position and rotation of the existing game object
            Vector3 currentPosition = transform.position;
            Quaternion currentRotation = transform.rotation;
            
            Destroy(gameObject);

            _newObject.layer = _layer;
            Instantiate(_newObject, currentPosition, currentRotation);
        }
    }
}