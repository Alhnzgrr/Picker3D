using DG.Tweening;
using Picker3D.Road;
using Picker3D.Scripts.General;
using UnityEngine;

namespace Picker3D.Scripts.Road
{
    public class StageRoad : MonoBehaviour, IStage
    {
        [SerializeField] private int collectAmount;
        [SerializeField] private GameObject stagePart;
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

        public void CollectAmountUpdate()
        {
            totalAmount++;
            if (totalAmount >= collectAmount)
            {
                StageComplete();
            }
        }

        private void StageComplete()
        {
            stagePart.transform.DOMoveY(-0.51f, 1f).OnComplete(() =>
            {
                _eventData.OnStageCompete?.Invoke();
            });
        }
    }
}
