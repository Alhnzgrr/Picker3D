using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

namespace Picker3D.Scripts.Road
{
    public class RoadController : MonoBehaviour
    {
        [SerializeField] private List<Transform> points;
        [SerializeField] private bool isStage;

        public bool IsStage
        {
            get => isStage;
            set => isStage = value;
        }

        public List<Transform> Points
        {
            get => points;
            set => points = value;
        }

        public bool IsInteraction { get; private set; }

        public void InteractionPlayer()
        {
            StartCoroutine(InteractionPlayerCoroutine());
        }

        private IEnumerator InteractionPlayerCoroutine()
        {
            IsInteraction = true;
            
            yield return new WaitForSeconds(10);
            
            IsInteraction = false;
        }
    }
}