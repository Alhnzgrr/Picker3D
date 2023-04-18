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

        private EventData _eventData;

        

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

        // private void Awake()
        // {
        //     _eventData = Resources.Load("EventData") as EventData;
        // }
        //
        // private void OnEnable()
        // {
        //     _eventData.OnResetValues += OnResetValues;
        // }
        //
        // private void OnDisable()
        // {
        //     _eventData.OnResetValues -= OnResetValues;
        // }

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