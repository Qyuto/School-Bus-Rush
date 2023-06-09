using UnityEngine;
using UnityEngine.AI;

namespace People
{
    public class Agent
    {
        public Animator animator { get; set; }
        public NavMeshAgent meshAgent { get; set; }

        public Agent(Animator animator, NavMeshAgent navMeshAgent)
        {
            this.animator = animator;
            meshAgent = navMeshAgent;
        }

        public void SetDestination(Vector3 destination)
        {
            meshAgent.SetDestination(destination);
            SetMoveAnimation();
        }

        public void SetMoveAnimation()
        {
            if(animator != null)
                animator.SetTrigger("Walk");
        }
    }
}