using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;

        NavMeshAgent navMeshAgent;
        Health health;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>(); //makes sure nav mech is applyed to player at the start
            health = GetComponent<Health>();
        }

        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator(); //in update as constantly using rest animation
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionSchedular>().StartAction(this);//puts action into the schedular
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;//sets point on nav mech clicked as the destination
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z; //local velocity is relative to are as opposed to regular velocity
            GetComponent<Animator>().SetFloat("forwardSpeed", speed); //calls the animator
        }
    }
}