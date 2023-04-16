using System.Collections.Generic;
using Picker3D.Scripts.Collectable;
using UnityEngine;

namespace Picker3D.Scripts.Player
{
    public class CollectableListController : MonoBehaviour
    {
        private List<Collectable.Collectable> collectables = new List<Collectable.Collectable>();

        public void AddList(Collectable.Collectable collectable)
        {
            collectables.Add(collectable);
        }

        public void RemoveList(Collectable.Collectable collectable)
        {
            collectables.Remove(collectable);
        }

        public void StageAreaAction()
        {
            foreach (var collectable in collectables)
            {
                collectable.Throw();
            }
        }
    }
}
