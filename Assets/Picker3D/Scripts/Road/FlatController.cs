using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.Utilities;
using UnityEngine;

namespace Picker3D.Scripts.Road
{
    public class FlatController : MonoBehaviour
    {
        [SerializeField] private GameObject pyramidCollectables;
        [SerializeField] private GameObject cubeCollectables;
        [SerializeField] private GameObject emojiCollectables;
        [SerializeField] private GameObject sphereCollectables;
        [SerializeField] private FlatCollectableType flatCollectableType;

        private List<Collectable.Collectable> collectables = new List<Collectable.Collectable>();

        private void Start()
        {
            AddCollectable();
        }

        private GameObject GetCollectable()
        {
            return flatCollectableType switch
            {
                FlatCollectableType.Cube => cubeCollectables,
                FlatCollectableType.Pyramid => pyramidCollectables,
                FlatCollectableType.Emoji => emojiCollectables,
                FlatCollectableType.Sphere => sphereCollectables,
                _ => null
            };
        }

        public void AddCollectable()
        {
            GetCollectable().SetActive(true);
            
            collectables.AddRange(GetCollectable().GetComponentsInChildren<Collectable.Collectable>());
            
            SetVisibility();
            GetCollectable().SetActive(false);
        }

        public void SetVisibility()
        {
            foreach (var collectable in collectables)
            {
                collectable.transform.localScale = Vector3.zero;
            }
        }

        public void SetActivateCollectables()
        {
            GetCollectable().SetActive(true);
            
            foreach (var collectable in collectables)
            {
                collectable.gameObject.SetActive(true);
                collectable.Activate();
            }
        }
    }

    public enum FlatCollectableType
    {
        Cube,
        Emoji,
        Pyramid,
        Sphere
    }
}
