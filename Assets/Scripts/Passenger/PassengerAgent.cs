using System;
using Skins;
using UnityEngine;
using UnityEngine.AI;

namespace Passenger
{
    public class PassengerAgent : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent meshAgent;
        [SerializeField] private LoadMeshSkin meshSkin;

        private Action<PassengerAgent> _killAgent;

        private void Reset()
        {
            animator = GetComponent<Animator>();
            meshAgent = GetComponent<NavMeshAgent>();
            meshSkin = GetComponent<LoadMeshSkin>();
        }

        public void InitAgent(Action<PassengerAgent> actionOnKillAgent)
        {
            _killAgent = actionOnKillAgent;
            LoadAgentSkin();
        }

        public void StartAgent(Vector3 destination)
        {
            SetMoveAnimation();
            meshAgent.SetDestination(destination);
        }

        public void KillAgent()
        {
            _killAgent?.Invoke(this);
        }

        private void LoadAgentSkin()
        {
            if (meshSkin == null) return;
            meshSkin.LoadSKin();
        }

        private void SetMoveAnimation()
        {
            if (animator == null) return;
            animator.SetTrigger("Walk");
        }

        private void OnDisable()
        {
            KillAgent();
        }
    }
}