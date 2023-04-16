using System;
using DG.Tweening;
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
            Vector3 pos = transform.position + Vector3.forward * 20 + Vector3.up * 0.5f;
            _rigidbody.DOJump(pos, 1, 1, 0.75f);
            // _rigidbody.AddForce(Vector3.forward * 75 , ForceMode.Impulse);
        }

        public void IsInArea(bool value)
        {
            InPlayerArea = value;
        }

        public void IsInStageArea(bool value)
        {
            InStageArea = value;
        }

        public void Activate()
        {
            transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
        }
    }
}

