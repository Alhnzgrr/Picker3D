using UnityEngine;

namespace Picker3D.LevelSystem
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Vector3 lastPosition;
        [SerializeField] private float angle;

        public Vector3 LastPosition
        {
            get => lastPosition + transform.position;
            set => lastPosition = value;
        }

        public float Angle
        {
            get => angle;
            set => angle = value;
        }
    }
}
