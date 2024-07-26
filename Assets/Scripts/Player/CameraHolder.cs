using UnityEngine;
using Zenject;

namespace Player
{
    public class CameraHolder : MonoBehaviour
    {
        private Transform _target;
        private Vector3 _initialRotation;

        [Inject]
        private void Construct(PlayerInteraction player)
        {
            _target = player.transform;
        }

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