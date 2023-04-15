using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Picker3D.LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Level[] levels;
        [SerializeField] private float levelsObjectScaleFactor;
        [SerializeField] private int randomLevelLowerLimit;

        private Level _lastLevel;

        private int _lastLevelIndex;
        
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
        public int Stage
        {
            get => PlayerPrefs.GetInt("Stage", 1);
            set => PlayerPrefs.SetInt("Stage", value);
        }

        private void Start()
        {
            int level = Level;
            _lastLevelIndex = level;
            levels[level].gameObject.SetActive(true);
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