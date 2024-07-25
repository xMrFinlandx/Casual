using UnityEngine;

namespace Player
{
    public class CameraHolder : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private Vector3 _initialRotation;

        private void Awake()
        {
            _initialRotation = transform.eulerAngles;
        }

        private void LateUpdate()
        {
            transform.position = _target.position;
            transform.eulerAngles = new Vector3(_target.eulerAngles.x + _initialRotation.x, _target.eulerAngles.y + _initialRotation.y);
        }
    }
}