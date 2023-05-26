using UnityEngine;
using UnityEngine.Events;

namespace Bus
{
    public class BusCollector : MonoBehaviour
    {
        [SerializeField] private Vector3 searchExtend;
        [SerializeField] private LayerMask peopleMask;
        [SerializeField] private LayerMask obstacleMask;

        private readonly Collider[] _overlapFind = new Collider[10];
        public UnityEvent<int> onBusCollectPassenger;
        public UnityEvent<int> onBusLostPassenger;
        
        private void Update()
        {
            TryCollectPeople();
            TryCollectObstacle();
        }

        private void TryCollectPeople()
        {
            int peopleSize = Physics.OverlapBoxNonAlloc(transform.position, searchExtend / 2, _overlapFind,
                transform.rotation ,peopleMask);
            
            if (peopleSize == 0) return;
            for (int i = 0; i != peopleSize; i++)
            {
                Destroy(_overlapFind[i].gameObject);
                _overlapFind[i] = null;
            }
            
            onBusCollectPassenger?.Invoke(peopleSize);
        }

        private void TryCollectObstacle()
        {
            int obstacleSize = Physics.OverlapBoxNonAlloc(transform.position, searchExtend / 2, _overlapFind,
                transform.rotation ,obstacleMask);
            Debug.Log(obstacleSize);
            if(obstacleSize == 0) return;
            
            for (int i = 0; i != obstacleSize; i++)
            {
                Destroy(_overlapFind[i].gameObject);
                _overlapFind[i] = null;
            }
            onBusLostPassenger?.Invoke(obstacleSize);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position,searchExtend);
        }
    }
}