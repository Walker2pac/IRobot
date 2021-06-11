using PathCreation;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class MonoAttachToPath : MonoBehaviour
    {
        public PathCreator path;
        [OnValueChanged("UpdateView")]
        public Vector2 offset;

        [NonSerialized]
        public Vector3 startPosition;
        private Vector2 startOffset;
        private Vector3 _editorPrevPosition;
        private Quaternion _editorPrevRotation;

        public void Start()
        {
            startPosition = transform.position;
            startOffset = offset;
            //UpdateView();
        }

        private void OnDrawGizmos()
        {
            if (path != null && !Application.isPlaying)
            {
                if (_editorPrevRotation != transform.rotation || _editorPrevPosition != transform.position)
                {
                    UpdateView();
                }
            }
        }

        private void UpdateView()
        {
            float distance = path.path.GetClosestDistanceAlongPath(transform.position);
            Vector3 direction = path.path.GetDirectionAtDistance(distance, EndOfPathInstruction.Stop);
            Vector3 normal = path.path.GetNormalAtDistance(distance, EndOfPathInstruction.Stop);
            Vector3 vertical = Vector3.Cross(direction, normal);
            Quaternion rotation = path.path.GetRotationAtDistance(distance);
            rotation = Quaternion.AngleAxis(90f, direction) * rotation;
            Vector3 position = path.path.GetClosestPointOnPath(transform.position);
            position += offset.x * normal + offset.y * vertical;
            transform.position = position;
            transform.rotation = rotation;

            _editorPrevPosition = transform.position;
            _editorPrevRotation = transform.rotation;
        }
    }
}