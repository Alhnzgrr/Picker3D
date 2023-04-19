using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Picker3D.Scripts.Player
{
    public class PlayerSkillController : MonoBehaviour
    {
        [SerializeField] private SkillType skillType;
        [SerializeField] private float skillActiveTime;
        [FormerlySerializedAs("rotateSpeed")] [SerializeField] private float rotateForce;

        private Rigidbody _rigidbody;
        private float defaultSkillActiveTime;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            if (skillActiveTime > 0)
            {
                skillActiveTime -= Time.deltaTime;
            }
            else
            {
                skillActiveTime = defaultSkillActiveTime;
                gameObject.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            if (skillType == SkillType.Left)
            {
                _rigidbody.AddTorque(Vector3.up * rotateForce , ForceMode.Impulse);
            }
            else
            {
                _rigidbody.AddTorque(Vector3.up * -rotateForce , ForceMode.Impulse);
            }
        }

        public void TimeUpdate(float value)
        {
            defaultSkillActiveTime = value;
            skillActiveTime = value;
        }
    }

    public enum SkillType
    {
        Left,
        Right
    }
}