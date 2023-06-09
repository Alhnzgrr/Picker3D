using System;
using System.Collections;
using System.Collections.Generic;
using Picker3D.Scripts.Collectable;
using Picker3D.Scripts.General;
using Picker3D.Scripts.Player;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Picker3D.Scripts.Road
{
    public class FlatController : MonoBehaviour
    {
        [Header("Collectables")]
        [SerializeField] private FlatCollectableType flatCollectableType;
        [SerializeField] private GameObject pyramidCollectables;
        [SerializeField] private GameObject cubeCollectables;
        [SerializeField] private GameObject emojiCollectables;
        [SerializeField] private GameObject sphereCollectables;
        [SerializeField] private GameObject duckCollectables;
        [SerializeField] private GameObject helicopterCollectables;
        [SerializeField] private GameObject breakerSphereCollectable;

        [Header("SKILL")]
        [SerializeField] private bool skillUsing;
        [SerializeField] private SkillPosition skillPosition;
        [SerializeField] private GameObject playerSkillObject;
        [SerializeField] private Vector3 skillStartPosition;
        [SerializeField] private Vector3 skillMiddlePosition;
        
        [Header("OTHER SETTINGS")]
        [SerializeField] private Transform respawnTransform;
        public Transform RespawnTransform => respawnTransform;

        private EventData _eventData;
        private HelicopterCollectableController _helicopterCollectableController;
        private BreakerSphereController _breakerSphereController;

        private List<Collectable.Collectable> collectables = new List<Collectable.Collectable>();

        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            _eventData.OnResetValues += ResetRoad;
            
            GetCollectable().SetActive(true);
            GetSkillPosition();
        }

        private void Start()
        {
            if (IsConstantCollectableType())
            {
                collectables.AddRange(GetCollectable().GetComponentsInChildren<Collectable.Collectable>());
            }
        }

        private void OnDisable()
        {
            _eventData.OnResetValues -= ResetRoad;
        }

        private GameObject GetCollectable()
        {
            return flatCollectableType switch
            {
                FlatCollectableType.Cube => cubeCollectables,
                FlatCollectableType.Pyramid => pyramidCollectables,
                FlatCollectableType.Emoji => emojiCollectables,
                FlatCollectableType.Sphere => sphereCollectables,
                FlatCollectableType.Duck => duckCollectables,
                FlatCollectableType.BreakerSphere => breakerSphereCollectable,
                FlatCollectableType.Helicopter => helicopterCollectables,

                _ => null
            };
        }

        private bool IsConstantCollectableType()
        {
            return flatCollectableType != FlatCollectableType.Helicopter &&
                   flatCollectableType != FlatCollectableType.BreakerSphere;
        }

        public void ResetRoad()
        {
            foreach (var collectable in collectables)
            {
                collectable.GetBackDefaultPosition();
            }
            
            GetSkillPosition();
        }

        private void GetSkillPosition()
        {
            if (!skillUsing) return;

            playerSkillObject.transform.localPosition = skillPosition == SkillPosition.Start ? skillStartPosition : skillMiddlePosition;
            
            playerSkillObject.gameObject.SetActive(true);
        }

        public void GetFlatType(FlatCollectableType type)
        {
            flatCollectableType = type;
        }

        public void GetSkillPositionType(SkillPosition type , bool value)
        {
            skillUsing = value;
            skillPosition = type;
        }
    }

    
}
