using Picker3D.Scripts.General;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Picker3D.LevelSystem
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        [SerializeField] private Level[] levels;
        [SerializeField] private float levelsObjectScaleFactor;
        [SerializeField] private int randomLevelLowerLimit;

        private Level _lastLevel;

        private int _lastLevelIndex;
        private EventData _eventData;

        public int Level
        {
            get => PlayerPrefs.GetInt("Level");
            set => PlayerPrefs.SetInt("Level", value);
        }

        private int Stage
        {
            get => PlayerPrefs.GetInt("Stage", 1);
            set => PlayerPrefs.SetInt("Stage", value);
        }

        protected override void Awake()
        {
            base.Awake();
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            _eventData.OnStageCompete += OnStageCompete;
            _eventData.OnFinishLevel += OnFinishLevel;
        }

        private void Start()
        {
            _lastLevelIndex = _level;
            GetNextLevel(ref _level);
            levels[_level].gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _eventData.OnStageCompete -= OnStageCompete;
            _eventData.OnFinishLevel -= OnFinishLevel;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                NextLevel();
            }
        }

        private void OnStageCompete()
        {
            Stage++;
            if (Stage >= 3)
            {
                Stage = 0;
            }
        }

        private void OnFinishLevel()
        {
            NextLevel();
        }

        private int _level;

        private void NextLevel()
        {
            _lastLevelIndex = _level;
            Level++;
            GetNextLevel(ref _level);

            levels[_level].gameObject.SetActive(true);

            levels[_level].transform.position = levels[_lastLevelIndex].LastPosition.position;
        }

        private void GetNextLevel(ref int currentLevel)
        {
            if (Level >= levels.Length)
            {
                int tempLevel = currentLevel;
                while (tempLevel == currentLevel)
                {
                    // Get level until not equal to last level when level returning random

                    currentLevel = Random.Range(0, levels.Length);
                }
            }
            else
            {
                currentLevel = Level;
            }
        }
    }
}