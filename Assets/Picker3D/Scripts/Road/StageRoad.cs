using System;
using System.Collections;
using DG.Tweening;
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
        [SerializeField] private GameObject door;
        [SerializeField] private int collectAmount;
        [SerializeField] private UnityEvent stageCompleteUnityEvent;
        [SerializeField] private TextMeshPro amountText;

        private EventData _eventData;

        private int totalAmount;

        public int CollectAmount
        {
            get => collectAmount;
            set => collectAmount = value;
        }

        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            amountText.text = $"{totalAmount} / {collectAmount}";
        }

        private void OnDisable()
        {
            totalAmount = 0;
        }

        public void CollectAmountUpdate()
        {
            totalAmount++;
            amountText.text = $"{totalAmount} / {collectAmount}";
            if (totalAmount >= collectAmount)
            {
                StageComplete();
            }
        }

        public void StageComplete()
        {
            transform.DOLocalMoveY(0f, 1f).OnComplete(() =>
            {
                StartCoroutine(StageCompleteCoroutine());
            });
        }

        private IEnumerator StageCompleteCoroutine()
        {
            yield return new WaitForSeconds(0.5f);

            door.transform.DOLocalMoveX(2, 0.25f);
            stageCompleteUnityEvent?.Invoke();
            _eventData.OnStageCompete?.Invoke();
        }
    }
}
