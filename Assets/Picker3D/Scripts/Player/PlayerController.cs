using System;
using Picker3D.Scripts.General;
using Picker3D.Scripts.Movement;
using Picker3D.Scripts.Road;
using TMPro;
using UnityEngine;

namespace Picker3D.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private EventData _eventData;
        private VerticalMove _verticalMove;
        private CollectableListController _collectableListController;

        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
            _verticalMove = GetComponent<VerticalMove>();
            _collectableListController = GetComponent<CollectableListController>();
        }

        private void OnEnable()
        {
            _eventData.OnStageCompete += OnStageCompete;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Collectable.Collectable collectable) && !collectable.InPlayerArea)
            {
                _collectableListController.AddList(collectable);
                collectable.IsInArea(true);
            }
            if(other.TryGetComponent(out RoadController roadController))
            {
                if (roadController.IsInteraction) return;

                if (roadController.IsStage)
                {
                    _verticalMove.CanMove(false);
                    _collectableListController.StageAreaAction();
                }
                
                roadController.InteractionPlayer();
                _verticalMove.WayPointUpdate(roadController.Points);
            }

            if (other.TryGetComponent(out FlatController flatController))
            {
                flatController.SetActivateCollectables();
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Collectable.Collectable collectable) && collectable.InPlayerArea)
            {
                _collectableListController.RemoveList(collectable);
                collectable.IsInArea(false);
            }
        }

        private void OnDisable()
        {
            _eventData.OnStageCompete -= OnStageCompete;
        }

        private void OnStageCompete()
        {
            _verticalMove.CanMove(true);
        }
    }
}