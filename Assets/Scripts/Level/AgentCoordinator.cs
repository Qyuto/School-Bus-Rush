using System.Collections;
using System.Collections.Generic;
using Bus;
using Skins;
using Passenger;
using UnityEngine;
using UnityEngine.AI;

namespace Level
{
    public class AgentCoordinator : MonoBehaviour, IPassengerModifier
    {
        [SerializeField] private NavMeshAgent agentPrefab;
        [SerializeField] private Transform agentSpawnTransform;
        [SerializeField] private PassengerCount passengerCount;
        [SerializeField] private BusLevelCompletion levelCompletion;

        private readonly List<PassengerAgent> _agentPool = new List<PassengerAgent>(1000);
        private Vector3 _agentDestination;

        private void Awake()
        {
            passengerCount.onBusLostPassenger.AddListener(DestroyAgents);
            passengerCount.onBusCollectPassenger.AddListener(CreateAgents);
            levelCompletion.onBusArrivedAtEnd.AddListener(() => StartCoroutine(EnableAgents()));
        }

        private void CreateAgents(IPassengerModifier modifier)
        {
            for (int i = 0; i < modifier.GetPassengerCount(); i++)
            {
                NavMeshAgent agent = Instantiate(agentPrefab, agentSpawnTransform.position,
                    agentSpawnTransform.rotation);

                agent.gameObject.SetActive(false);
                _agentPool.Add(new PassengerAgent(agent.GetComponent<Animator>(), agent,
                    agent.GetComponent<LoadMeshSkin>()));
            }
        }

        private void DestroyAgents(IPassengerModifier modifier)
        {
            int size = modifier.GetPassengerCount();
            if (size > _agentPool.Count)
            {
                Debug.LogError("The number of deleted passengers is greater than the current one");
                return;
            }

            for (int i = 0; i < size; i++) Destroy(_agentPool[i].meshAgent.gameObject);
            _agentPool.RemoveRange(0, size);
        }

        private IEnumerator EnableAgents()
        {
            passengerCount.onBusLostPassenger.RemoveListener(DestroyAgents);
            foreach (var agent in _agentPool)
            {
                yield return new WaitForSeconds(Time.fixedDeltaTime);

                agent.meshAgent.transform.position = agentSpawnTransform.position;
                agent.meshAgent.gameObject.SetActive(true);
                agent.StartAgent(_agentDestination);
                passengerCount.onBusLostPassenger?.Invoke(this);
            }
        }

        public void SetAgentDestination(Vector3 target)
        {
            _agentDestination = target;
        }

        public int GetPassengerCount() => 1;

        public string GetModifierType() => "AgentCoordinator";
    }
}