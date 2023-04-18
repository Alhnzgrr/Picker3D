using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Picker3D.Scripts.General;
using UnityEngine;

namespace Picker3D.Scripts.Collectable
{
    public class BreakerSphere : MonoBehaviour
    {
        [SerializeField] private GameObject bigSphere;
        [SerializeField] private GameObject[] collectables;

        private EventData _eventData;
        private Vector3 bigSphereDefaultPosition;

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
                _collectable.InstantlyGetBackDefaultPosition();
                
                collectable.SetActive(false);
            }
            
            bigSphere.transform.localPosition = bigSphereDefaultPosition;
            bigSphere.SetActive(true);
        }
    }
}