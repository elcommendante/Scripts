
using UnityEngine;
using GameClient.Combat;
using GameClient.Core;
using GameClient.Movement;
using System;
using UnityEngine.AI;

namespace GameClient.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        private float waypointDwellTime = 3f;
       
        // [SerializeField] float chaseSpeed = 4.5f;
        [SerializeField] float spawnMaxDistance = 10f;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f; 


        Fighter fighter;
        Health health;
        GameObject player;
        Mover mover;
        NavMeshAgent navMeshAgent;

        Vector3 guardPosition;
        Vector3 spawnPoint;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            spawnPoint = transform.position;

            guardPosition = transform.position;
        }
        private void Update()
        {
            if (health.IsDead()) return;
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)
            {
                if(AtWaypoint())
                {
                    waypointDwellTime = GenerateFloat(3,7); // Generates new float for dwelling time
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }


            if(timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
            
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
            if (spawnMaxDistance < Vector3.Distance(transform.position, spawnPoint))
            {
                Debug.Log("Ty smieciu, gdzie uciekasz");
                fighter.Cancel();
                PatrolBehaviour();
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }


        // Called by Unity - will show what the chase distance is
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        float GenerateFloat(float x, float y) // Generates a float in desired range 
        {
            return UnityEngine.Random.Range(x,y);
        }
            

    }
}
