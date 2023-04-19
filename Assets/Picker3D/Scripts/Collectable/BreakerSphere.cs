using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Picker3D.Scripts.General;
using Picker3D.Scripts.Player;
using UnityEngine;

namespace Picker3D.Scripts.Collectable
{
    public class BreakerSphere : MonoBehaviour
    {
        [SerializeField] private GameObject bigSphere;
        [SerializeField] private GameObject[] collectables;

        private EventData _eventData;
        private Vector3 bigSphereDefaultPosition;
        
        private bool _isActive = false;

        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            _eventData.OnResetValues += OnEndTask;
        }

        private void OnDisable()
        {
            _eventData.OnResetValues -= OnEndTask;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < 10f)
            {
                if(_isActive) return;
                
                OnStartTask();
                _isActive = true;
            }
        }

        public void OnStartTask()
        {
            bigSphereDefaultPosition = bigSphere.transform.localPosition;
            bigSphere.SetActive(false);
            
            foreach (var collectable in collectables)
            {
                collectable.SetActive(true);
            }
        }
        
        private void OnEndTask()
        {
            foreach (var collectable in collectables)
            {
                Collectable _collectable = collectable.GetComponent<Collectable>();
                collectable.gameObject.SetActive(true);
                
                _collectable.GetRigidbodyForBreaberSphere();
                _collectable.InstantlyGetBackDefaultPosition();
                
                collectable.SetActive(false);
            }
            
            bigSphere.transform.localPosition = bigSphereDefaultPosition;
            bigSphere.SetActive(true);
            _isActive = false;
        }
    }
}