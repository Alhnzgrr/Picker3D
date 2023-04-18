using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using Picker3D.Road;
using Picker3D.Scripts.General;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Picker3D.Scripts.Road
{
    public class StageRoad : MonoBehaviour, IStage
    {
        [SerializeField] private GameObject leftDoor , rightDoor;
        [SerializeField] private int collectAmount;
        [SerializeField] private UnityEvent stageCompleteUnityEvent;
        [SerializeField] private TextMeshPro amountText;

        private EventData _eventData;
        private RoadController _roadController;

        private int _totalAmount;
        private bool _stageComplete = false;

        public int CollectAmount
        {
            get => collectAmount;
            set => collectAmount = value;
        }

        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
            _roadController = GetComponentInParent<RoadController>();
        }

        private void OnEnable()
        {
            _eventData.OnResetValues += OnResetValues;
            amountText.text = $"{_totalAmount} / {collectAmount}";
        }

        private void OnDisable()
        {
            _eventData.OnResetValues += OnResetValues;
            _totalAmount = 0;
        }

        public void CollectAmountUpdate()
        {
            _totalAmount++;
            amountText.text = $"{_totalAmount} / {collectAmount}";
            if (_totalAmount >= collectAmount && !_stageComplete)
            {
                _stageComplete = true;
                StartCoroutine(StageCompleteCoroutine());
            }
        }

        public void CheckTotalAmount()
        {
            StartCoroutine(CheckTotalAmountCoroutine());
        }

        private IEnumerator CheckTotalAmountCoroutine()
        {
            
            yield return new WaitForSeconds(3f);

            if (_totalAmount < collectAmount)
            {
                _eventData.OnLoseLevel?.Invoke();
            }
        }

        private IEnumerator StageCompleteCoroutine()
        {
            yield return new WaitForSeconds(1f);
            
            transform.DOLocalMoveY(0f, 0.5f).OnComplete(() =>
            {
                StartCoroutine(OnEndTastCoroutine());
            });
        }

        private IEnumerator OnEndTastCoroutine()
        {
            yield return new WaitForSeconds(0.5f);

            DoorOpenAction();
            stageCompleteUnityEvent?.Invoke();
            _eventData.OnStageCompete?.Invoke();

            if (_roadController.IsFinish)
            {
                _eventData.OnFinishLevel?.Invoke();
            }
        }
        
        private void OnResetValues()
        {
            StartCoroutine(ResetStage());
        }
        
        private IEnumerator ResetStage()
        {
            yield return new WaitForSeconds(5);

            _totalAmount = 0;
            leftDoor.transform.rotation = Quaternion.identity;
            rightDoor.transform.rotation = Quaternion.identity;
            transform.localPosition = Vector3.up * -0.5f;
            amountText.text = $"{_totalAmount} / {collectAmount}";
            _stageComplete = false;
        }
            
        private void DoorOpenAction()
        {
            leftDoor.transform.DORotate(Vector3.forward * 60, .5f);
            rightDoor.transform.DORotate(Vector3.forward * -60, .5f);
        }
    }
}
