using System;
using System.Collections;
using System.Collections.Generic;
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

        public void IsInStageArea()
        {
            StartCoroutine(ResetStageAreaCoroutine());
        }

        private IEnumerator ResetStageAreaCoroutine()
        {
            InStageArea = true;
            
            yield return new WaitForSeconds(5);
            
            InStageArea = false;
        }

        public void GetBackDefaultPosition()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            
            transform.rotation = Quaternion.identity;
            transform.localPosition = _spawnPosition;
        }

        public void InstantlyGetBackDefaultPosition()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            transform.localPosition = _spawnPosition;
        }

        public void GetRigidbodyForBreaberSphere()
        {
            if(!_rigidbody)  _rigidbody = GetComponent<Rigidbody>();
        }

        public void DestroyAfterLifeTime()
        {
            Destroy(gameObject , 20);
        }
    }
}

