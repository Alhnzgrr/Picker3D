using System;
using System.Collections;
using DG.Tweening;
using Picker3D.Scripts.General;
using UnityEngine;

namespace Picker3D.Scripts.Collectable
{
    public class Collectable : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public bool InPlayerArea { get; private set; } = false;
        public bool InStageArea { get; private set; } = false;

        private Vector3 _spawnPosition;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _spawnPosition = transform.localPosition;
        }

        public void Throw()
        {
            _rigidbody.AddForce(Vector3.forward * 45 , ForceMode.Impulse);
        }

        public void IsInArea(bool value)
        {
            InPlayerArea = value;
        }

        public void IsInStageArea(bool value)
        {
            InStageArea = value;
        }

        public void DestroyAfterLifeTime()
        {
            Destroy(gameObject , 20);
        }
    }
}

