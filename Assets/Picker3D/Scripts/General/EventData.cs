using System;
using UnityEngine;

namespace Picker3D.Scripts.General
{
    [CreateAssetMenu(fileName = "EventData", menuName = "Data/Event Data")]
    public class EventData : ScriptableObject
    {
        public Action OnPlay { get; set; }
        public Action OnFinishLevel { get; set; }
        public Action OnLoseLevel { get; set; }
        public Action OnStageCompete { get; set; }
        public Action OnResetValues{ get; set; }
        public Action OnPlayerSkill { get; set; }
    }
}
