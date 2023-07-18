using UnityEngine;

namespace Obstacles
{
    public class ObstacleDestroy : MonoBehaviour
    {
        [SerializeField] private bool destroyGameObject;
        private Obstacle _obstacle;

        private void Awake()
        {
            _obstacle = GetComponent<Obstacle>();
            if (_obstacle != null)
                _obstacle.onInteractSelect += DestroyObstacle;
        }

        private void DestroyObstacle(IBusInteractor obj)
        {
            _obstacle.onInteractSelect -= DestroyObstacle;
            if (destroyGameObject) Destroy(_obstacle.gameObject);
            else Destroy(_obstacle);
        }
    }
}