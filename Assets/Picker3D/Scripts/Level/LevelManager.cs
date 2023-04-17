using System;
using Picker3D.Scripts.General;
using Picker3D.Scripts.Road;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private int Level
        {
            get => PlayerPrefs.GetInt("Level") > levels.Length
                ? Random.Range(randomLevelLowerLimit, levels.Length)
                : PlayerPrefs.GetInt("Level", 1);
            set
            {
                PlayerPrefs.SetInt("RealLevel", value);
                if (levels.Length >= value)
                {
                    PlayerPrefs.SetInt("Level", value);
                }
            }
        }
        public int RealLevel => PlayerPrefs.GetInt("RealLevel", 1);

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
        }

        private void Start()
        {
            var level = Level;
            _lastLevelIndex = level;
            levels[level].gameObject.SetActive(true);
        }
        
        private void OnDisable()
        {
            _eventData.OnStageCompete -= OnStageCompete;
        }
        
        private void OnStageCompete()
        {
            Stage++;
            if (Stage >= 3)
            {
                Stage = 0;
            }
        }

        private void NextLevel()
        {
            Level++;
            int levelIndex = Level;
            _lastLevelIndex = levelIndex;

            levels[levelIndex].gameObject.SetActive(true);

            if (levelIndex != 0)
            {
                levels[levelIndex].transform.position = levels[_lastLevelIndex].LastPosition * levelsObjectScaleFactor;
            }

        }
    }
}