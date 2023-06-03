using DG.Tweening;
using UnityEngine;

namespace Obstacles
{
    public class MotionObstacle : DivideObstacle
    {
        [Range(1, 10), SerializeField] private float randomFactor;
        [SerializeField] private Vector3 firstVectorMoveDirection;
        [SerializeField] private Vector3 secondVectorMoveDirection;
        [SerializeField] private float moveSpeed;

        [SerializeField] private bool rotation;
        [SerializeField] private float speedRotation;

        private void Awake()
        {
            moveSpeed += Random.Range(-randomFactor, randomFactor);
            speedRotation += Random.Range(-randomFactor, randomFactor);

            firstVectorMoveDirection += transform.position;
            secondVectorMoveDirection += transform.position;
        }

        private void Start()
        {
            if (firstVectorMoveDirection == Vector3.zero && secondVectorMoveDirection == Vector3.zero) return;
            transform.DOMove(secondVectorMoveDirection, moveSpeed).onComplete += MoveToFirst;
        }

        private void FixedUpdate()
        {
            if (rotation)
                transform.rotation *= Quaternion.Euler(0, 0, speedRotation);
        }

        private void MoveToFirst()
        {
            transform.DOMove(firstVectorMoveDirection, moveSpeed).onComplete += MoveToSecond;
        }

        private void MoveToSecond()
        {
            transform.DOMove(secondVectorMoveDirection, moveSpeed).onComplete += MoveToFirst;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(firstVectorMoveDirection + transform.position, 0.4f);
            Gizmos.DrawWireSphere(secondVectorMoveDirection + transform.position, 0.4f);
            Gizmos.DrawLine(transform.position + firstVectorMoveDirection,
                transform.position + secondVectorMoveDirection);
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}