using Skins;
using UnityEngine;
using UnityEngine.AI;

namespace Passenger
{
    public class PassengerAgent
    {
        public Animator animator { get; set; }
        public NavMeshAgent meshAgent { get; set; }
        public ChangeMesh changeMesh { get; set; }

        public PassengerAgent(Animator animator, NavMeshAgent navMeshAgent, ChangeMesh changeMesh)
        {
            this.animator = animator;
            meshAgent = navMeshAgent;
            this.changeMesh = changeMesh;
        }

        public void SetDestination(Vector3 destination)
        {
            meshAgent.SetDestination(destination);
            SetMoveAnimation();
        }

        public void SetMoveAnimation()
        {
            if (animator == null) return;
            changeMesh.ChangeModel(SkinDataCollection.Instance.passengerSkin);
            animator.SetTrigger("Walk");
        }
    }
}