using System;
using DG.Tweening;
using Dreamteck.Splines.Primitives;
using Picker3D.Helper;
using Picker3D.Scripts.General;
using Picker3D.Scripts.Movement;
using Picker3D.Scripts.Road;
using UnityEngine;

namespace Picker3D.Scripts.Player
{
    public class PlayerController : MonoSingleton<MonoBehaviour>
    {
        private EventData _eventData;
        private PlayerMovement _playerMovement;
        private CollectableListController _collectableListController;
        private Transform _respawmTransform;
        private Vector3 defaultPosition;

        protected override void Awake()
        {
            base.Awake();
            _eventData = Resources.Load("EventData") as EventData;
            _playerMovement = GetComponent<PlayerMovement>();
            _collectableListController = GetComponent<CollectableListController>();
        }

        private void OnEnable()
        {
            _eventData.OnStageCompete += OnStageCompete;
            _eventData.OnLoseLevel += OnLevelFailed;
            _eventData.OnPlay += OnStageCompete;
        }

        private void Start()
        {
            defaultPosition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Collectable.Collectable collectable) && !collectable.InPlayerArea)
            {
                _collectableListController.AddList(collectable);
                collectable.IsInArea(true);
            }

            if (other.TryGetComponent(out Road.RoadController roadController))
            {
                
                if (roadController.IsInteraction) return;
                
                if (roadController.IsStage)
                {
                    if (CatchHelper.TryGetComponentThisOrChild(other.gameObject, out StageRoad stageRoad))
                    {
                        stageRoad.CheckTotalAmount();
                    }

                    _playerMovement.CanMove(false);
                    _collectableListController.StageAreaAction();
                }
                else
                {
                    if (CatchHelper.TryGetComponentThisOrChild(other.gameObject, out FlatController flatController))
                    {
                        flatController.SetActivateCollectables();
                        flatController.ResetRoad();
                        _respawmTransform = flatController.RespawnTransform;
                    }
                }

                roadController.InteractionPlayer();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Collectable.Collectable collectable) && collectable.InPlayerArea)
            {
                _collectableListController.RemoveList(collectable);
                collectable.IsInArea(false);
            }
            
            if(other.TryGetComponent(out RoadController roadController))
            {
                roadController.OnResetValues();
            }
        }

        private void OnDisable()
        {
            _eventData.OnStageCompete -= OnStageCompete;
            _eventData.OnLoseLevel -= OnLevelFailed;
            _eventData.OnPlay -= OnStageCompete;
        }

        private void OnStageCompete()
        {
            _playerMovement.CanMove(true);
        }

        private void OnLevelFailed()
        {
            transform.position = _respawmTransform ? 
                new Vector3(0, transform.position.y, _respawmTransform.position.z) : defaultPosition;
        }
    }
}