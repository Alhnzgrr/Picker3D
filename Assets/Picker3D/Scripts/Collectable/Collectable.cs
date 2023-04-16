using System;
using UnityEngine;

namespace Picker3D.Scripts.Collectable
{
    public class Collectable : MonoBehaviour
    {
        Rigidbody _rigidbody;
        public bool InPlayerArea { get; private set; } = false;
        
        public bool InStageArea { get; private set; } = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Throw()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.forward * 75 , ForceMode.Impulse);
        }

        public void IsInArea(bool value)
        {
            InPlayerArea = value;
        }

        public void IsInStageArea(bool value)
        {
            InStageArea = value;
        }
    }
}

