using UnityEngine;

namespace Picker3D.Scripts.General
{
    public class FgEnum : MonoBehaviour
    {
        public enum GameState
        {
            Idle,
            Play,
            Finish,
            Lose
        }
        
        public enum FlatCollectableType
        {
            Cube,
            Emoji,
            Pyramid,
            Sphere,
            Duck,
            Helicopter,
            BreakerSphere
        }
    }
}
