using System;
using UnityEngine;

namespace Obstacles
{
    public class ObstacleDestroy : MonoBehaviour
    {
        private Obstacle _obstacle;

        private void Awake()
        {
            _obstacle = GetComponent<Obstacle>();
            if(_obstacle != null)
                _obstacle.onInteractSelect += DestroyObstacle;
        }

        private void DestroyObstacle(IBusInteractor obj)
        {
            _obstacle.onInteractSelect -= DestroyObstacle;
            Destroy(_obstacle.gameObject);
        }
    }
}