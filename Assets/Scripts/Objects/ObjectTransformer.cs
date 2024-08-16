using System;
using UnityEngine;

namespace Objects
{
    public class ObjectTransformer : MonoBehaviour
    {
        [SerializeField] private GameObject _newObject;
        [SerializeField] private LayerMask _layer;

        private string _layerValue;

        private void Start()
        {
            int layerInt = (int) Mathf.Log(_layer.value, 2);
            
            _layerValue = LayerMask.LayerToName(layerInt);
        }

        public void TransformToObject()
        {
            // Store the current position and rotation of the existing game object
            Vector3 currentPosition = transform.position;
            Quaternion currentRotation = transform.rotation;
            
            Destroy(gameObject);

            _newObject.layer = LayerMask.NameToLayer(_layerValue);
            Instantiate(_newObject, currentPosition, currentRotation);
        }
    }
}