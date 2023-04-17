using System;
using System.Collections;
using System.Collections.Generic;
using Picker3D.Scripts.Collectable;
using Picker3D.Scripts.General;
using UnityEngine;
using UnityEngine.Serialization;

namespace Picker3D.Scripts.Road
{
    public class FlatController : MonoBehaviour
    {
        [SerializeField] private GameObject pyramidCollectables;
        [SerializeField] private GameObject cubeCollectables;
        [SerializeField] private GameObject emojiCollectables;
        [SerializeField] private GameObject sphereCollectables;
        [SerializeField] private GameObject duckCollectables;
        [SerializeField] private GameObject helicopterCollectables;
        [SerializeField] private GameObject breakerSphereCollectable;
        
        [SerializeField] private FgEnum.FlatCollectableType flatCollectableType;

        private HelicopterCollectableController _helicopterCollectableController;
        private BreakerSphereController _breakerSphereController;

        private List<Collectable.Collectable> collectables = new List<Collectable.Collectable>();

        private void OnEnable()
        {
            GetCollectable().SetActive(true);
            //AddCollectable();
        }

        private GameObject GetCollectable()
        {
            return flatCollectableType switch
            {
                FgEnum.FlatCollectableType.Cube => cubeCollectables,
                FgEnum.FlatCollectableType.Pyramid => pyramidCollectables,
                FgEnum.FlatCollectableType.Emoji => emojiCollectables,
                FgEnum.FlatCollectableType.Sphere => sphereCollectables,
                FgEnum.FlatCollectableType.Duck => duckCollectables,
                FgEnum.FlatCollectableType.BreakerSphere => breakerSphereCollectable,
                FgEnum.FlatCollectableType.Helicopter => helicopterCollectables,

                _ => null
            };
        }

        private void AddCollectable()
        {
            if (collectables.Count > 0)
            {
                for (int i = 0; i < collectables.Count; i++)
                {
                    Collectable.Collectable collectable = collectables[0];
                    collectables.Remove(collectable);
                }
            }
            
            collectables.AddRange(GetCollectable().GetComponentsInChildren<Collectable.Collectable>());
            
        }

        public void SetActivateCollectables()
        {
            switch (flatCollectableType)
            {
                case FgEnum.FlatCollectableType.Helicopter:
                {
                    if (!_helicopterCollectableController)
                    {
                        _helicopterCollectableController = GetComponentInChildren<HelicopterCollectableController>();
                    }
                
                    _helicopterCollectableController.OnStartTastHelicopter();
                    
                    break;
                    
                }
                case FgEnum.FlatCollectableType.BreakerSphere:
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
    }

    
}
