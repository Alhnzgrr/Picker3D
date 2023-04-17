using System;
using Dreamteck.Splines;
using Picker3D.Game;
using Picker3D.LevelSystem;
using Picker3D.Scripts.General;
using Picker3D.Scripts.Road;
using UnityEngine;

namespace Picker3D.Scripts.Movement
{
    public class PlayerMovementWithRigidbody : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotateSpeed;

        private Rigidbody _rigidbody;

        private bool _canMove = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            //if(!GameManager.Instance.PlayAbility()) return;

            Vector3 velocity = transform.right * (UIController.Instance.GetHorizontal() * rotateSpeed);
            velocity.y = _rigidbody.velocity.y;

            _rigidbody.velocity = velocity;
            
            if(!_canMove) return;
            
            //_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, moveSpeed);
        }

        public void CanMove(bool value)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _canMove = value;
        }
    }
}