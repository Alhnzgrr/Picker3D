using System;
using System.Collections;
using Picker3D.Scripts.General;
using Picker3D.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

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
                yield return new WaitForSeconds(0.25f);

                breakerSpheres[i].OnStartTask();
                
            }
        }
    }
}
