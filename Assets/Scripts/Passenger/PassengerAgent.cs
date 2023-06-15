using System;
using Skins;
using UnityEngine;
using UnityEngine.AI;

namespace Passenger
{
    public class PassengerAgent
    {
        public Animator animator { get; set; }
        public NavMeshAgent meshAgent { get; set; }

        private LoadMeshSkin _meshSkin;

        public PassengerAgent(Animator animator, NavMeshAgent navMeshAgent, LoadMeshSkin meshSkin)
        {
            this.animator = animator;
            meshAgent = navMeshAgent;
            _meshSkin = meshSkin;
        }

        public void StartAgent(Vector3 destination)
        {
            LoadAgentSkin();
            SetMoveAnimation();
            meshAgent.SetDestination(destination);
        }

        private void LoadAgentSkin()
        {
            if (_meshSkin == null) return;
            _meshSkin.LoadSKin();
        }

        private void SetMoveAnimation()
        {
            if (animator == null) return;
            animator.SetTrigger("Walk");
        }
    }
}