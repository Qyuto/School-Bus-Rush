using System.Collections;
using System.Collections.Generic;
using Bus;
using UnityEngine;
using UnityEngine.AI;

namespace Level
{
    public class AgentCoordinator : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agentPrefab;
        [SerializeField] private Transform agentSpawnTransform;
        [SerializeField] private PassengerCount passengerCount;
        [SerializeField] private BusLevelCompletion levelCompletion;

        private readonly List<NavMeshAgent> _agentPool = new List<NavMeshAgent>(1000);
        private Vector3 _agentDestination;

        private void Awake()
        {
            passengerCount.onBusLostPassenger.AddListener(DestroyAgents);
            passengerCount.onBusCollectPassenger.AddListener(CreateAgents);
            levelCompletion.onBusArrivedAtEnd.AddListener(() => StartCoroutine(EnableAgents()));
        }

        private void CreateAgents(int size)
        {
            for (int i = 0; i < size; i++)
            {
                NavMeshAgent agent = Instantiate(agentPrefab, agentSpawnTransform.position,
                    agentSpawnTransform.rotation);
                agent.gameObject.SetActive(false);
                _agentPool.Add(agent);
            }
        }

        private void DestroyAgents(int size)
        {
            if (size > _agentPool.Count)
            {
                Debug.LogError("The number of deleted passengers is greater than the current one");
                return;
            }

            for (int i = 0; i < size; i++) Destroy(_agentPool[i].gameObject);
            _agentPool.RemoveRange(0, size);
        }

        private IEnumerator EnableAgents()
        {
            passengerCount.onBusLostPassenger.RemoveListener(DestroyAgents);
            foreach (NavMeshAgent agent in _agentPool)
            {
                yield return new WaitForSeconds(Time.fixedDeltaTime);
                agent.transform.position = agentSpawnTransform.position;
                agent.gameObject.SetActive(true);
                agent.SetDestination(_agentDestination);
                passengerCount.onBusLostPassenger?.Invoke(1);
            }
        }
        
        public void SetAgentDestination(Vector3 target)
        {
            _agentDestination = target;
        }
    }
}