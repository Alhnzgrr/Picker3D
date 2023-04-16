using UnityEngine;
using UnityEngine.Serialization;

namespace Picker3D.LevelEditor
{
    [CreateAssetMenu(fileName = "PlatformData", menuName = "Data/Platform Data")]
    public class PlatformData : ScriptableObject
    {
        [SerializeField] private GameObject flatRoad;
        [SerializeField] private GameObject leftCornerRoad;
        [SerializeField] private GameObject rightCornerRoad;
        [SerializeField] private GameObject stageRoad;
        [SerializeField] private GameObject finishRoad;

        public GameObject FlatRoad => flatRoad;
        public GameObject LeftCornerRoad => leftCornerRoad;
        public GameObject RightCornerRoad => rightCornerRoad;
        public GameObject StageRoad => stageRoad;
        public GameObject FinishRoad => finishRoad;
    }
}
