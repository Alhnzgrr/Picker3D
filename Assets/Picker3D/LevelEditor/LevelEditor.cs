using System;
using Picker3D.Helper;
using Picker3D.Road;
using Picker3D.LevelSystem;
using Picker3D.Scripts.Road;
using Sirenix.OdinInspector;
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
            Corner,
            Stage,
            Finish
        }

        [SerializeField] private PlatformData platformData;
        [SerializeField] private RoadType roadType;

        [SerializeField, Min(4)] [ShowIf("roadType", RoadType.Flat)]
        private int lenght;

        [SerializeField, Min(5)] [ShowIf("IsStage")]
        private int stageNecessaryAmount;

        [SerializeField] [ShowIf("roadType", RoadType.Corner)]
        private DirectionType cornerDirection;

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
                case RoadType.Corner:

                    if (cornerDirection == DirectionType.Left)
                    {
                        newObject =
                            PrefabUtility.InstantiatePrefab(platformData.LeftCornerRoad, _levelX.transform) as GameObject;
                    }
                    else
                    {
                        newObject =
                            PrefabUtility.InstantiatePrefab(platformData.RightCornerRoad, _levelX.transform) as GameObject;
                    }

                    if (newObject != null)
                    {
                        // newObject.transform.rotation = cornerDirection == DirectionType.Left
                        //     ? Quaternion.Euler(Vector3.up * (0 + _currentAngle))
                        //     : Quaternion.Euler(Vector3.up * (270 + _currentAngle));

                        if (cornerDirection == DirectionType.Left)
                        {
                            newObject.transform.rotation = Quaternion.Euler(Vector3.up * (0 + _currentAngle));
                            _currentAngle -= 90;
                        }
                        else
                        {
                            newObject.transform.rotation = Quaternion.Euler(Vector3.up * (270 + _currentAngle));
                            _currentAngle += 90;
                        }

                        float distance = _lastObjectScale + 4;
                        newObject.transform.localPosition = _lastObjectLocalPosition + _direction * distance;

                        _direction = RotateDirection(cornerDirection);
                        _lastObjectLocalPosition = newObject.transform.localPosition;
                        _lastPosition = newObject.transform.position + _direction * 4;
                        _lastObjectScale = 4;
                    }

                    break;
                case RoadType.Flat:

                    newObject = PrefabUtility.InstantiatePrefab(platformData.FlatRoad, _levelX.transform) as GameObject;

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
                        _lastPosition = newObject.transform.position + _direction * newObject.transform.localScale.z;
                    }

                    break;
                case RoadType.Stage:

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

                        if (CatchHelper.TryGetComponentThisOrChild(newObject, out Scripts.Road.RoadController roadController))
                        {
                            roadController.IsStage = true;
                        }

                        _lastObjectLocalPosition = newObject.transform.localPosition;
                        _lastObjectScale = newObject.transform.localScale.z;
                        _lastPosition = newObject.transform.position + _direction * newObject.transform.localScale.z;
                    }

                    break;
                case RoadType.Finish:

                    newObject =
                        PrefabUtility.InstantiatePrefab(platformData.FinishRoad, _levelX.transform) as GameObject;

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
                        
                        if (CatchHelper.TryGetComponentThisOrChild(newObject, out Scripts.Road.RoadController roadController))
                        {
                            roadController.IsStage = true;
                            
                        }

                        _lastObjectLocalPosition = newObject.transform.localPosition;
                        _lastObjectScale = newObject.transform.localScale.z;
                        _lastPosition = newObject.transform.position + _direction * newObject.transform.localScale.z;
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
            level.LastPosition = _lastPosition;

            _levelX.transform.parent = null;
            _levelX = null;
            _lastObjectLocalPosition = Vector3.zero;
            _lastObjectScale = 0;
            _direction = Vector3.forward;
            _currentAngle = 0;
            _lastPosition = Vector3.zero;
            _stageIndex = 0;
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