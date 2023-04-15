using UnityEngine;

namespace Picker3D.LevelEditor
{
    [CreateAssetMenu(fileName = "PlatformData", menuName = "Data/Platform Data")]
    public class PlatformData : ScriptableObject
    {
        [SerializeField] private GameObject flatRoad;
        [SerializeField] private GameObject cornerRoad;
        [SerializeField] private GameObject stageRoad;
        [SerializeField] private GameObject finishRoad;

        public GameObject FlatRoad => flatRoad;
        public GameObject CornerRoad => cornerRoad;
        public GameObject StageRoad => stageRoad;
        public GameObject FinishRoad => finishRoad;
    }
}
