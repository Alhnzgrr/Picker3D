using System;
using Picker3D.Game;
using UnityEngine;

namespace Picker3D.Scripts.Movement
{
    public class HorizontalMove : MonoBehaviour
    {
        [SerializeField] private float horizontalMoveSpeed;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector3(UIController.Instance.GetHorizontal() * horizontalMoveSpeed,
                _rigidbody.velocity.y, _rigidbody.velocity.z);
        }
    }
}
