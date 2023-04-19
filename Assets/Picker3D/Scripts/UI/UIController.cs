using DG.Tweening;
using Picker3D.LevelSystem;
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
        [SerializeField] private Button playAgainButton;

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
            playAgainButton.onClick?.AddListener(PlayAgainButtonClick);
            
            _eventData.OnStageCompete += OnStageComplete;
            _eventData.OnLoseLevel += OnLevelFailed;
            _eventData.OnFinishLevel += OnFinish;
        }

        private void Start()
        {
            joystick.gameObject.SetActive(false);

            TextUpdate();
        }

        private void OnDisable()
        {
           playButton.onClick?.RemoveListener(PlayButtonClick);
           nextLevelButton.onClick?.RemoveListener(NextLevelButtonClick);
           playAgainButton.onClick?.RemoveListener(PlayAgainButtonClick);
           
           _eventData.OnStageCompete -= OnStageComplete;
           _eventData.OnLoseLevel -= OnLevelFailed;
           _eventData.OnFinishLevel -= OnFinish;
        }

        private void PlayAgainButtonClick()
        {
            losePanel.SetActive(false);
            startPanel.SetActive(true);
            
            _eventData.OnResetValues?.Invoke();
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

            NextLevelUpdates();
            gamePanel.SetActive(true);
            levelCompetePanel.SetActive(false);
            joystick.gameObject.SetActive(true);
            
        }
        
        private void OnFinish()
        {
            levelCompetePanel.SetActive(true);
            gamePanel.SetActive(false);
            joystick.gameObject.SetActive(false);
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
            losePanel.SetActive(true);
            gamePanel.SetActive(false);
            joystick.gameObject.SetActive(false);
        }

        public float GetHorizontal()
        {
            return joystick.Horizontal;
        }

        private void NextLevelUpdates()
        {
            TextUpdate();
            
            stageIndex = 0;
            
            foreach (var image in stageImages)
            {
                image.color = Color.white;
            }
        }

        private void TextUpdate()
        {
            int level = LevelManager.Instance.Level + 1;
            int nextLevel = level + 1;
            
            levelText.text = level.ToString();
            nextLevelText.text = nextLevel.ToString();
        }
    }
}
