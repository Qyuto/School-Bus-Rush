using System.Collections;
using Bus;
using Passenger;
using UnityEngine;
using UnityEngine.Pool;

namespace Level
{
    public class AgentCoordinator : MonoBehaviour, IPassengerModifier
    {
        [SerializeField] private int maxPoolSize;
        [SerializeField] private PassengerAgent agentPrefab;
        [SerializeField] private Transform agentSpawnTransform;
        [SerializeField] private PassengerCount passengerCount;
        [SerializeField] private BusLevelCompletion levelCompletion;

        private ObjectPool<PassengerAgent> _agentObjectPool;
        private int _passengerSpawnCount;
        private Vector3 _agentDestination;

        private void Awake()
        {
            _agentObjectPool = new ObjectPool<PassengerAgent>(CreateAgentFunc, ActionOnGetAgent, ActionOnReleaseAgent,
                ActionOnDestroyAgent);
            passengerCount.onBusLostPassenger.AddListener(DestroyAgents);
            passengerCount.onBusCollectPassenger.AddListener(CreateAgents);
            levelCompletion.onBusArrivedAtEnd.AddListener(() => StartCoroutine(EnableAgents()));
        }

        private void ActionOnKillAgent(PassengerAgent obj)
        {
            _agentObjectPool.Release(obj);
        }

        private void ActionOnDestroyAgent(PassengerAgent obj)
        {
            Destroy(obj.gameObject);
        }

        private void ActionOnReleaseAgent(PassengerAgent obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void ActionOnGetAgent(PassengerAgent obj)
        {
            obj.transform.position = agentSpawnTransform.position;
            obj.gameObject.SetActive(true);
            obj.StartAgent(_agentDestination);
        }

        private PassengerAgent CreateAgentFunc()
        {
            PassengerAgent agent = Instantiate(agentPrefab, agentSpawnTransform.position, agentSpawnTransform.rotation);
            agent.InitAgent(ActionOnKillAgent);
            return agent;
        }

        private void CreateAgents(IPassengerModifier modifier)
        {
            _passengerSpawnCount += modifier.GetPassengerCount();
        }

        private void DestroyAgents(IPassengerModifier modifier)
        {
            int size = modifier.GetPassengerCount();
            if (size > _passengerSpawnCount)
            {
                Debug.LogError("The number of deleted passengers is greater than the current one");
                return;
            }

            _passengerSpawnCount -= size;
        }

        private IEnumerator EnableAgents()
        {
            passengerCount.onBusLostPassenger.RemoveListener(DestroyAgents);
            while (_passengerSpawnCount != 0)
            {
                int spawnSize = 0;
                if (_agentObjectPool.CountAll == 0) spawnSize = maxPoolSize;
                else if (_agentObjectPool.CountActive == maxPoolSize) spawnSize = 0;
                else spawnSize = _agentObjectPool.CountInactive;

                if (_agentObjectPool.CountAll > maxPoolSize)
                    Debug.LogError($"Override agent pool limit: {_agentObjectPool.CountAll}");
                for (int i = 0; i < spawnSize && _passengerSpawnCount != 0; i++, _passengerSpawnCount--)
                {
                    _agentObjectPool.Get();
                    passengerCount.onBusLostPassenger?.Invoke(this);
                    yield return new WaitForFixedUpdate();
                }
                yield return null;
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