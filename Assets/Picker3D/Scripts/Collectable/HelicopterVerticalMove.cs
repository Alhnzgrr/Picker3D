using System;
using Dreamteck.Splines.Primitives;
using UnityEngine;

namespace Picker3D.Scripts.Collectable
{
    public class HelicopterVerticalMove : MonoBehaviour
    {
        [SerializeField] private float verticalMoveSpeed;
        public bool CanMove { get; set; } = false;

        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.localPosition;
        }

        private void Update()
        {
            if(!CanMove) return;

            transform.position += Vector3.forward * (verticalMoveSpeed * Time.deltaTime);
        }

        public void GetBackStartPosition()
        {
            transform.localPosition = _startPosition;
        }
    }
}
