using System;
using System.Collections;
using System.Collections.Generic;
using Picker3D.Scripts.Collectable;
using Picker3D.Scripts.General;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Picker3D.Scripts.Road
{
    public class FlatController : MonoBehaviour
    {
        [Header("Collectables")]
        [SerializeField] private GameObject pyramidCollectables;
        [SerializeField] private GameObject cubeCollectables;
        [SerializeField] private GameObject emojiCollectables;
        [SerializeField] private GameObject sphereCollectables;
        [SerializeField] private GameObject duckCollectables;
        [SerializeField] private GameObject helicopterCollectables;
        [SerializeField] private GameObject breakerSphereCollectable;
        
        [SerializeField] private FlatCollectableType flatCollectableType;

        private HelicopterCollectableController _helicopterCollectableController;
        private BreakerSphereController _breakerSphereController;

        private List<Collectable.Collectable> collectables = new List<Collectable.Collectable>();

        private void OnEnable()
        {
            GetCollectable().SetActive(true);
        }

        private void Start()
        {
            if (IsConstantCollectableType())
            {
                collectables.AddRange(GetCollectable().GetComponentsInChildren<Collectable.Collectable>());
            }
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

        public void SetActivateCollectables()
        {
            switch (flatCollectableType)
            {
                case FlatCollectableType.Helicopter:
                {
                    if (!_helicopterCollectableController)
                    {
                        _helicopterCollectableController = GetComponentInChildren<HelicopterCollectableController>();
                    }

                    _helicopterCollectableController.OnStartTastHelicopter();
                    break;
                }
                case FlatCollectableType.BreakerSphere:
                {
                    if (!_breakerSphereController)
                    {
                        _breakerSphereController =
                            GetComponentInChildren<BreakerSphereController>();
                    }

                    _breakerSphereController.OnStartTaskBreakerSphere();
                    break;
                }
            }
        }

        public void ResetRoad()
        {
            foreach (var collectable in collectables)
            {
                collectable.GetBackDefaultPosition();
            }
        }
    }

    
}
