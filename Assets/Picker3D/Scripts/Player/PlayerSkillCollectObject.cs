using System;
using Picker3D.Scripts.General;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Picker3D.Scripts.Player
{
    public class PlayerSkillCollectObject : MonoBehaviour
    {
        [SerializeField] private float skillObjectMoveSpeed;

        private EventData _eventData;
        private bool _direction = true;

        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void Update()
        {
            if (_direction)
            {
                transform.Translate(Vector3.right * (skillObjectMoveSpeed * Time.deltaTime)); 
            }
            else
            {
                transform.Translate(Vector3.left * (skillObjectMoveSpeed * Time.deltaTime));
            }

            if (transform.localPosition.x >= 5f)
            {
                _direction = false;
            }
            else if (transform.localPosition.x <= -5f) 
            {
                _direction = true; 
            }
        }

        public void PlayerCatchTheSkillObject()
        {
            _eventData.OnPlayerSkill?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
