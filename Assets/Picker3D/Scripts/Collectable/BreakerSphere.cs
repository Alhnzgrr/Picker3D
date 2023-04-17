using UnityEngine;

namespace Picker3D.Scripts.Collectable
{
    public class BreakerSphere : MonoBehaviour
    {
        [SerializeField] private GameObject bigSphere;
        [SerializeField] private GameObject[] collectables;

        public void OnStartTask()
        {
            bigSphere.SetActive(false);
            
            foreach (var collectable in collectables)
            {
                collectable.SetActive(true);
            }
        }
    }
}