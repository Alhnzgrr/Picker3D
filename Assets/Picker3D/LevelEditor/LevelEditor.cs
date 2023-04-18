using System;
using Picker3D.Helper;
using Picker3D.Road;
using Picker3D.LevelSystem;
using Picker3D.Scripts.Road;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Picker3D.LevelEditor
{
    public class LevelEditor : MonoBehaviour
    {
        private enum DirectionType
        {
            Right,
            Left
        }

        private enum RoadType
        {
            Flat,
            Stage,
            Finish
        }

        [SerializeField] private PlatformData platformData;
        [SerializeField] private RoadType roadType;

        [SerializeField, Min(4)] [ShowIf("roadType", RoadType.Flat)]
        private int lenght;

        [SerializeField, Min(5)] [ShowIf("IsStage")]
        private int stageNecessaryAmount;

        private bool IsStage => roadType == RoadType.Stage || roadType == RoadType.Finish;

        private GameObject _levelX = null;

        private Vector3 _lastObjectLocalPosition;
        private Vector3 _lastPosition;
        private Vector3 _direction = Vector3.forward;

        private int _index;
        private int _stageIndex;
        
        private float _currentAngle;
        private float _lastObjectScale;
        
        private bool _isFirstObject;
        private bool _levelComplete;
        private bool _isFlatAdded = true;

        [Button]
        private void ClearAll()
        {
            int count = transform.childCount;

            for (int i = 0; i < count; i++)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }

            if (_levelX != null)
            {
                _levelX = null;
            }

            _lastObjectLocalPosition = Vector3.zero;
            _lastObjectScale = 0;
            _direction = Vector3.forward;
            _currentAngle = 0;
            _lastPosition = Vector3.zero;
            _stageIndex = 0;
            _levelComplete = false;
            _isFlatAdded = true;
        }

        [Button]
        private void Add()
        {
            if (_levelX == null)
            {
                _levelX = new GameObject();
                _levelX.name = "LevelX";
                _levelX.transform.parent = transform;
                _levelX.transform.localPosition = Vector3.zero;
            }

            GameObject newObject = null;

            switch (roadType)
            {
                case RoadType.Flat:

                    if (!_levelComplete)
                    {
                        _isFlatAdded = true;
                        
                        newObject =
                            PrefabUtility.InstantiatePrefab(platformData.FlatRoad, _levelX.transform) as GameObject;

                        if (newObject != null)
                        {
                            Vector3 localScale = newObject.transform.localScale;
                            localScale = new Vector3(localScale.x, localScale.y, lenght);
                            newObject.transform.localScale = localScale;

                            float distance = _lastObjectScale + newObject.transform.localScale.z;
                            newObject.transform.localPosition = _lastObjectLocalPosition + _direction * distance;
                            newObject.transform.rotation = Quaternion.Euler(Vector3.up * _currentAngle);
                            _lastObjectLocalPosition = newObject.transform.localPosition;
                            _lastObjectScale = newObject.transform.localScale.z;
                            _lastPosition = newObject.transform.position +
                                            _direction * newObject.transform.localScale.z;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("You must save this level");
                    }

                    break;
                case RoadType.Stage:

                    if (!_levelComplete)
                    {
                        if (_stageIndex < 2)
                        {
                            if (_isFlatAdded)
                            {
                                _isFlatAdded = false;
                                _stageIndex++;
                                newObject = PrefabUtility.InstantiatePrefab(platformData.StageRoad,
                                    _levelX.transform) as GameObject;

                                if (newObject != null)
                                {
                                    Vector3 localScale = newObject.transform.localScale;
                                    newObject.transform.localScale = localScale;

                                    float distance = _lastObjectScale + newObject.transform.localScale.z;
                                    newObject.transform.localPosition = _lastObjectLocalPosition + _direction * distance;
                                    newObject.transform.rotation = Quaternion.Euler(Vector3.up * _currentAngle);

                                    if (CatchHelper.TryGetComponentThisOrChild(newObject, out IStage iStage))
                                    {
                                        iStage.CollectAmount = stageNecessaryAmount;
                                    }

                                    if (CatchHelper.TryGetComponentThisOrChild(newObject,
                                            out RoadController roadController))
                                    {
                                        roadController.IsStage = true;
                                    }

                                    _lastObjectLocalPosition = newObject.transform.localPosition;
                                    _lastObjectScale = newObject.transform.localScale.z;
                                    _lastPosition = newObject.transform.position +
                                                    _direction * newObject.transform.localScale.z;
                                }
                            }
                            else
                            {
                                Debug.LogWarning("You must add a flat");
                            }
                            
                        }
                        else
                        {
                            Debug.LogWarning("Stage Limit is full");
                        }
                        
                    }
                    else
                    {
                        Debug.LogWarning("You must save this level");
                    }


                    break;
                case RoadType.Finish:

                    if (!_levelComplete)
                    {
                        if (_stageIndex == 2)
                        {
                            if (_isFlatAdded)
                            {
                                _isFlatAdded = false;
                                _levelComplete = true;
                                newObject =
                                    PrefabUtility.InstantiatePrefab(platformData.FinishRoad, _levelX.transform) as
                                        GameObject;

                                if (newObject != null)
                                {
                                    Vector3 localScale = newObject.transform.localScale;
                                    newObject.transform.localScale = localScale;

                                    float distance = _lastObjectScale + newObject.transform.localScale.z;
                                    newObject.transform.localPosition = _lastObjectLocalPosition + _direction * distance;
                                    newObject.transform.rotation = Quaternion.Euler(Vector3.up * _currentAngle);

                                    if (CatchHelper.TryGetComponentThisOrChild(newObject, out IStage iStage))
                                    {
                                        iStage.CollectAmount = stageNecessaryAmount;
                                    }

                                    if (CatchHelper.TryGetComponentThisOrChild(newObject,
                                            out Scripts.Road.RoadController roadController))
                                    {
                                        roadController.IsStage = true;
                                        roadController.IsFinish = true;
                                    }

                                    _lastObjectLocalPosition = newObject.transform.localPosition;
                                    _lastObjectScale = newObject.transform.localScale.z;
                                    _lastPosition = newObject.transform.position +
                                                    _direction * newObject.transform.localScale.z;
                                }
                            }
                            else
                            {
                                Debug.LogWarning("You must add a flat");
                            }
                            
                        }
                        else
                        {
                            Debug.LogWarning("Stage amount is not enough for level end");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("You must save this level");
                    }


                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [Button]
        private void Save()
        {
            Level level = _levelX.AddComponent<Level>();
            level.Angle = _currentAngle;
            
            Transform newTransform = new GameObject("RefEndPosition").transform;
            newTransform.parent = level.transform;
            newTransform.position = _lastPosition;
            level.LastPosition = newTransform;

            _levelX.transform.parent = null;
            _levelX = null;
            _lastObjectLocalPosition = Vector3.zero;
            _lastObjectScale = 0;
            _direction = Vector3.forward;
            _currentAngle = 0;
            _lastPosition = Vector3.zero;
            _stageIndex = 0;
            _levelComplete = false;
            _isFlatAdded = true;
        }

        private Vector3 RotateDirection(DirectionType directionType)
        {
            if (directionType == DirectionType.Right)
            {
                if (_direction == Vector3.forward)
                {
                    return Vector3.right;
                }

                if (_direction == Vector3.right)
                {
                    return Vector3.back;
                }

                if (_direction == Vector3.back)
                {
                    return Vector3.left;
                }

                if (_direction == Vector3.left)
                {
                    return Vector3.forward;
                }
            }
            else
            {
                if (_direction == Vector3.forward)
                {
                    return Vector3.left;
                }

                if (_direction == Vector3.right)
                {
                    return Vector3.forward;
                }

                if (_direction == Vector3.back)
                {
                    return Vector3.right;
                }

                if (_direction == Vector3.left)
                {
                    return Vector3.back;
                }
            }

            return Vector3.forward;
        }
    }
}