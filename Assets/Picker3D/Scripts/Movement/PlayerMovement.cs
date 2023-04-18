using System.Collections.Generic;
using Picker3D.Game;
using Picker3D.Scripts.General;
using UnityEngine;
using UnityEngine.Serialization;

namespace Picker3D.Scripts.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float verticalSpeed;
        [SerializeField] private float horizontalSpeed;

        private Rigidbody _rigidbody;

        private bool _canMove = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!GameManager.Instance.PlayAbility() || !_canMove)
            {
                _rigidbody.velocity = new Vector3(UIController.Instance.GetHorizontal() * horizontalSpeed, 0, 0);
                return;
            }

            _rigidbody.velocity = new Vector3(UIController.Instance.GetHorizontal() * horizontalSpeed, 0, verticalSpeed);
        }

        public void CanMove(bool value)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _canMove = value;
        }
    }
}