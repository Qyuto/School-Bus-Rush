using Bus;
using UnityEngine;

namespace Obstacles
{
    public class OpenableObstacle : Obstacle
    {
        [SerializeField] private float deltaMove;
        [SerializeField] private Transform movableTransform;
        [SerializeField] private BusTrigger busTrigger;

        private bool _canMove;
        private Vector3 _targetPosition;

        private void Awake()
        {
            _targetPosition = movableTransform.position + Vector3.down * 20f;
            busTrigger.onTriggerEnter.AddListener(StartOpen);
        }

        private void FixedUpdate()
        {
            if (_canMove) OpenObstacle();
        }

        private void StartOpen(GameObject bus)
        {
            Debug.Log("Start open");
            _canMove = true;
        }
        
        private void OpenObstacle()
        {
            movableTransform.position =
                Vector3.Lerp(movableTransform.position, _targetPosition, deltaMove * Time.fixedDeltaTime);
            if ((_targetPosition - movableTransform.position).sqrMagnitude < 10) _canMove = false;
        }
    }
}