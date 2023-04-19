using System;
using System.Collections;
using DG.Tweening;
using Picker3D.Scripts.General;
using Picker3D.Scripts.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Picker3D.Scripts.Collectable
{
    public class HelicopterCollectableController : MonoBehaviour
    {
        [SerializeField] private Collectable pyramidCollectable;
        [SerializeField] private float helicopterSpawnTime;
        [SerializeField] private float helicopterSpeed;
        [SerializeField, Min(20), MaxValue(30)] private int spawnAmount;

        private HelicopterVerticalMove _helicopterVerticalMove;
        
        private bool _direction = true;
        private bool _canMove;

        private void Awake()
        {
            _helicopterVerticalMove = GetComponentInParent<HelicopterVerticalMove>();
        }

        private void Update()
        {
            if (!_canMove)
            {
                if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < 12.5f)
                {
                    _canMove = true;
                    OnStartTastHelicopter();
                }
            }
            
            if(!_canMove) return;
            
            if (_direction)
            {
                transform.Translate(Vector3.right * (helicopterSpeed * Time.deltaTime)); 
            }
            else
            {
                transform.Translate(Vector3.left * (helicopterSpeed * Time.deltaTime));
            }

            if (transform.localPosition.x >= 4.0f)
            {
                _direction = false;
            }
            else if (transform.localPosition.x <= -4.0f) 
            {
                _direction = true; 
            }
        }

        private void OnStartTastHelicopter()
        {
            StartCoroutine(OnStartTaskCoroutine());
        }

        private IEnumerator OnStartTaskCoroutine()
        {
            _canMove = true;
            _helicopterVerticalMove.CanMove = true;
            
            for (int i = 0; i < spawnAmount; i++)
            {
                Collectable collectable = Instantiate(pyramidCollectable , transform.position , Quaternion.identity);
                
                collectable.DestroyAfterLifeTime();

                yield return new WaitForSeconds(helicopterSpawnTime);
                
            }

            _canMove = false;
            _helicopterVerticalMove.GetBackStartPosition();
            _helicopterVerticalMove.CanMove = false;
        }
    }
}