using System;
using Dreamteck.Splines.Primitives;
using Picker3D.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

namespace Picker3D.Game
{
    public class UIController : MonoSingleton<UIController>
    {
        [Header("PANELS")]
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject levelCompetePanel;

        [Header("BUTTONS")]
        [SerializeField] private Button playButton;

        [Header("JOYSTICK")]
        [SerializeField] private Joystick joystick;
        
        private EventData _eventData;

        protected override void Awake()
        {
            base.Awake();
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            playButton.onClick?.AddListener(PlayButtonClick);
        }

        private void Start()
        {
            joystick.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
           playButton.onClick?.RemoveListener(PlayButtonClick);
        }
        
        private void PlayButtonClick()
        {
            _eventData.OnPlay?.Invoke();
            joystick.gameObject.SetActive(true);
            startPanel.gameObject.SetActive(false);
        }

        public float GetHorizontal()
        {
            return joystick.Horizontal;
        }
    }
}
