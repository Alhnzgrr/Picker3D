using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines.Primitives;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Picker3D.Scripts.Collectable
{
    public class BreakerSphereController : MonoBehaviour
    {
        [SerializeField] private BreakerSphere[] breakerSpheres;

        public void OnStartTaskBreakerSphere()
        {
            StartCoroutine(OnStartTaskCoroutine());
        }
        
        private IEnumerator OnStartTaskCoroutine()
        {
            for (int i = 0; i < breakerSpheres.Length; i++)
            {
                yield return new WaitForSeconds(1.75f);

                breakerSpheres[i].OnStartTask();
                
            }
        }
    }
}
