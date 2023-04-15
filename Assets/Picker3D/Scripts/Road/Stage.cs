using UnityEngine;

namespace Picker3D.Road
{
    public class Stage : MonoBehaviour, IStage
    {
        [SerializeField] private int collectAmount;

        public int CollectAmount
        {
            get => collectAmount;
            set => collectAmount = value;
        }
    }
}
