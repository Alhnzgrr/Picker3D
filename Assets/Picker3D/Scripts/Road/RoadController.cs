using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using Picker3D.Scripts.General;
using UnityEngine;

namespace Picker3D.Scripts.Road
{
    public class RoadController : MonoBehaviour
    {
        [SerializeField] private bool isStage;
        [SerializeField] private bool isFinish;

        public bool IsStage
        {
            get => isStage;
            set => isStage = value;
        }

        public bool IsFinish
        {
            get => isFinish;
            set => isFinish = value;
        }

        public bool IsInteraction { get; private set; }

        public void OnResetValues()
        {
            IsInteraction = false;
        }

        public void InteractionPlayer()
        {
            IsInteraction = true;
        }
    }
}