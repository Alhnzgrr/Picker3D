using System;
using DG.Tweening;
using Picker3D.Game;
using Picker3D.Scripts.General;
using UnityEngine;
using UnityEngine.Serialization;

namespace Picker3D.Scripts.Movement
{
    public class PlayerHorizontalMove : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        // private void Update()
        // {
        //     if(!GameManager.Instance.PlayAbility()) return;
        //     
        //     Move();
        // }
        //
        // private void Move()
        // {
        //     // Vector3 velocity = transform.right * (UIController.Instance.GetHorizontal() * moveSpeed);
        //     //
        //     // _rigidbody.velocity = velocity;
        //     
        //     transform.localPosition += transform.right * (UIController.Instance.GetHorizontal() * moveSpeed * Time.fixedDeltaTime);
        // }
    }
}