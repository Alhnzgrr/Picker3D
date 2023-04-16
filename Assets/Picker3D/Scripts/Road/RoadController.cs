using System;
using Dreamteck.Splines;
using UnityEngine;

namespace Picker3D.Scripts.Road
{
    public class RoadController : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private int stageIndex;
        [SerializeField] private bool isStage;
        [SerializeField] private bool isFlat;

        
        public bool IsStage
        {
            get => isStage;
            set => isStage = value;
        }

        public bool IsInteraction { get; private set; }

        public void InteractionPlayer()
        {
            IsInteraction = true;
        }

        public Transform[] Points => points;
    }
}