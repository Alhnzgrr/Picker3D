using System;
using UnityEngine;

namespace Picker3D.Scripts.General
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private EventData _eventData;
        public GameState GameStateType { get; set; } = GameState.Idle;

        protected override void Awake()
        {
            base.Awake();
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            _eventData.OnPlay += OnPlay;
            _eventData.OnFinishLevel += OnFinishLevel;
            _eventData.OnLoseLevel += OnLoseLevel;
        }
        private void OnDisable()
        {
            _eventData.OnPlay -= OnPlay;
            _eventData.OnFinishLevel -= OnFinishLevel;
            _eventData.OnLoseLevel -= OnLoseLevel;
        }

        public bool PlayAbility()
        {
            return GameStateType == GameState.Play;
        }

        private void OnLoseLevel()
        {
            GameStateType = GameState.Lose;
        }

        private void OnFinishLevel()
        {
            GameStateType = GameState.Finish;
        }

        private void OnPlay()
        {
            GameStateType = GameState.Play;
        }
    }
}
