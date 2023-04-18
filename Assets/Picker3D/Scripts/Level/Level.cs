using UnityEngine;

namespace Picker3D.LevelSystem
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform lastPosition;
        [SerializeField] private float angle;

        public Transform LastPosition
        {
            get => lastPosition;
            set => lastPosition = value;
        }

        public float Angle
        {
            get => angle;
            set => angle = value;
        }
    }
}
