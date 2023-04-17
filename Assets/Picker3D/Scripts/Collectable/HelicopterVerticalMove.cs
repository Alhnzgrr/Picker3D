using System;
using UnityEngine;

namespace Picker3D.Scripts.Collectable
{
    public class HelicopterVerticalMove : MonoBehaviour
    {
        [SerializeField] private float verticalMoveSpeed;
        public bool CanMove { get; set; } = false;

        private void Update()
        {
            if(!CanMove) return;

            transform.position += Vector3.forward * (verticalMoveSpeed * Time.deltaTime);
        }
    }
}
