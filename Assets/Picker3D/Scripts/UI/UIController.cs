using System;
using DG.Tweening;
using Dreamteck.Splines.Primitives;
using Picker3D.Scripts.General;
using TMPro;
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

        [Header("IMAGES")]
        [SerializeField] private Image[] stageImages;

        [Header("TEXTS")] 
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI nextLevelText;

        [Header("BUTTONS")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button nextLevelButton;

        [Header("JOYSTICK")]
        [SerializeField] private Joystick joystick;

        [Header("Image Scale Effect Settings")]
        [SerializeField] private Vector3 scaleEffectSize;
        [SerializeField] private float scaleEffectDuration;
        [SerializeField] private Ease scaleEffectEase;
        
        private EventData _eventData;
        
        private int stageIndex;

        protected override void Awake()
        {
            base.Awake();
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            playButton.onClick?.AddListener(PlayButtonClick);
            nextLevelButton.onClick?.AddListener(NextLevelButtonClick);
            
            _eventData.OnStageCompete += OnStageComplete;
            _eventData.OnLoseLevel += OnLevelFailed;
            _eventData.OnFinishLevel += OnFinish;
        }

        private void Start()
        {
            joystick.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
           playButton.onClick?.RemoveListener(PlayButtonClick);
           nextLevelButton.onClick?.RemoveListener(NextLevelButtonClick);
           
           _eventData.OnStageCompete -= OnStageComplete;
           _eventData.OnLoseLevel -= OnLevelFailed;
           _eventData.OnFinishLevel -= OnFinish;
        }

        private void PlayButtonClick()
        {
            _eventData.OnPlay?.Invoke();
            joystick.gameObject.SetActive(true);
            startPanel.SetActive(false);
            gamePanel.SetActive(true);
        }
        
        private void NextLevelButtonClick()
        {
            _eventData.OnPlay?.Invoke();
            _eventData.OnResetValues?.Invoke();
            
            gamePanel.SetActive(true);
            levelCompetePanel.SetActive(false);
            stageIndex = 0;
        }
        
        private void OnFinish()
        {
            levelCompetePanel.SetActive(true);
            gamePanel.SetActive(false);
        }
        
        private void OnStageComplete()
        {
            Image stageImage = stageImages[stageIndex];
            
            stageImage.transform.DOScale(scaleEffectSize, scaleEffectDuration).SetEase(scaleEffectEase)
                .OnComplete(
                    () =>
                    {
                        stageImage.transform.DOScale(Vector3.one, scaleEffectDuration)
                            .SetEase(scaleEffectEase);
                    });
            
            stageImages[stageIndex].color = Color.green;
            
            stageIndex++;
        }

        private void OnLevelFailed()
        {
            stageIndex = 0;

            foreach (var image in stageImages)
            {
                image.color = Color.white;
            }
        }

        public float GetHorizontal()
        {
            return joystick.Horizontal;
        }
    }
}
