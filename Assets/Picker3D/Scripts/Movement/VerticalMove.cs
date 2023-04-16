using System;
using Dreamteck.Splines;
using Picker3D.Game;
using Picker3D.LevelSystem;
using Picker3D.Scripts.Road;
using UnityEngine;

namespace Picker3D.Scripts.Movement
{
    public class VerticalMove : MonoBehaviour
    {
        [SerializeField] private RoadController firstRoad;
        [SerializeField] private float playerYPosition;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotateSpeed;

        private Transform[] wayPoints;
        private Rigidbody _rigidbody;

        private int _wayPointIndex = 0;
        private bool _canMove = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        // private void Start()
        // {
        //     wayPoints = firstRoad.Points;
        // }

        private void Update()
        {
            if(!_canMove) return;
            
            _rigidbody.velocity = new Vector3(UIController.Instance.GetHorizontal() * rotateSpeed,
                _rigidbody.velocity.y, moveSpeed);
        }

        public void CanMove(bool value)
        {
            _canMove = value;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        public void WayPointUpdate(Transform[] points)
        {
            wayPoints = null;
            wayPoints = points;
            _wayPointIndex = 0;
        }
      
        private void Move()
        {
            if(wayPoints == null) return;
            
            Vector3 wayPointPosition = new Vector3(wayPoints[_wayPointIndex].position.x, playerYPosition,
                wayPoints[_wayPointIndex].position.z);
            
            if (Vector3.Distance(transform.position, wayPointPosition) > 1f)
            {
                float moveStep = moveSpeed * Time.deltaTime;
                float rotateStep = rotateSpeed * Time.deltaTime;
                
                Vector3 direction = (wayPointPosition - transform.position).normalized;
                
                transform.position = Vector3.MoveTowards(transform.position, wayPointPosition, moveStep);
                
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateStep);
            }
            else
            {
                _wayPointIndex++;
            }
        }
    }
}