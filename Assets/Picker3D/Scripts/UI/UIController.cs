using Picker3D.Scripts.General;
using UnityEngine;

namespace Picker3D.Game
{
    public class UIController : MonoSingleton<UIController>
    {
        [SerializeField] private Joystick joystick;

        public float GetHorizontal()
        {
            return joystick.Horizontal;
        }
    }
}
