using System;
using Picker3D.Road;
using Picker3D.Scripts.Player;
using Picker3D.Scripts.Road;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Picker3D.Scripts.Collectable
{
    public class CollectableCounter : MonoBehaviour
    {
        private StageRoad _stageRoad;

        private void Awake()
        {
            _stageRoad = GetComponentInParent<StageRoad>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Collectable collectable))
            {
                if(collectable.InStageArea) return;
                
                collectable.IsInStageArea();
                _stageRoad.CollectAmountUpdate();
            }
        }
    }
}
