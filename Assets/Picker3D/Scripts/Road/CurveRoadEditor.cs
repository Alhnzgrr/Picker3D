using System;
using System.Collections.Generic;
using Picker3D.Helper;
using Picker3D.Scripts.Road;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Picker3D.Road
{
    public class CurveRoadEditor : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private Transform pointsParent;
        [SerializeField] private List<Transform> wayPoints;
        [SerializeField] private int count;
        [SerializeField] private float startAngle;
        [SerializeField] private bool showDebug;
        
        private float _radius;
        private float _angle;

        [Button]
        private void CreatePoints()
        {
            Clear();
            
            _radius = Mathf.Abs(startPoint.localPosition.x) < Mathf.Abs(endPoint.localPosition.z)
                ? Mathf.Abs(startPoint.localPosition.x)
                : Mathf.Abs(endPoint.localPosition.z);

            _angle = 90f / count;
            
            for (int i = 0; i <= count; i++)
            {
                float currentAngle = _angle * (count - i);
                
                float x = Mathf.Cos((startAngle + currentAngle) * Mathf.Deg2Rad) * _radius;
                float z = Mathf.Sin((startAngle + currentAngle) * Mathf.Deg2Rad) * _radius;

                Vector3 newPoint = new Vector3(x, 0, z);
                GameObject newObject = new GameObject
                {
                    transform =
                    {
                        parent = pointsParent,
                        localPosition = newPoint
                    }
                };
                wayPoints.Add(newObject.transform);
            }
        }

        [Button]
        private void Clear()
        {
            int childCount = pointsParent.childCount;
            
            wayPoints.Clear();
            
            for (int i = 0; i < childCount; i++)
            {
                DestroyImmediate(pointsParent.GetChild(0).gameObject);
            }
        }

        [Button]
        private void SavePoints()
        {
            if (CatchHelper.TryGetComponentThisOrChildOrParent(gameObject, out RoadController road))
            {
                road.Points.Clear();
                road.Points.AddRange(wayPoints.ToArray());
            }
            else
            {
                Debug.LogError("RoadController has been not found!");
            }
        }

        private void OnDrawGizmos()
        {
            if (!showDebug) return;

            foreach (Transform wayPoint in wayPoints)
            {
                Gizmos.DrawSphere(wayPoint.position, 0.2f);
            }
        }
    }
}
