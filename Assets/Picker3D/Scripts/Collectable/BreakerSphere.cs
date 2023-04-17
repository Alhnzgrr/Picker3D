using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Scripts.Collectable
{
    public class BreakerSphere : MonoBehaviour
    {
        [SerializeField] private GameObject bigSphere;
        [SerializeField] private GameObject[] collectables;

        private Vector3 bigSphereDefaultPosition;
        
        public void OnStartTask()
        {
            bigSphereDefaultPosition = bigSphere.transform.localPosition;
            bigSphere.SetActive(false);
            
            foreach (var collectable in collectables)
            {
                collectable.SetActive(true);
            }

            StartCoroutine(OnEndTaskCoroutine());
            
        }

        private IEnumerator OnEndTaskCoroutine()
        {
            yield return new WaitForSeconds(20);
            
            OnEndTask();
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