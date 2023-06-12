using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class FireEnemy : MonoBehaviour
    {        
        public float attackRadius = 7f;
        public float chaseRadius = 10f;
        public float patrolRadius = 30f;
        public float attackCooldown = 1f;
        public Rigidbody projectile;
        public float ForwardStrength = 0f;
        public float UpwardStrength = -20f;
        public float ChaseSpeed = 5f;

        public delegate void AttackTest();

        public AttackTest AttackMethod;
        public float ProjectileLifetime = 3f;

        private bool isInAttackRadius = false;
        private bool isInChaseRadius = false;
        private bool isPatrolling = false;
        private bool alreadyAttacked = false;
        private Transform player;
        private PlayerData playerData;
        private NavMeshAgent agent;

        // Start is called before the first frame update
        private void Awake()
        {
            player = GameObject.Find("Player").transform;
            playerData = player.GetComponent<PlayerData>();
            agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(player.position);
        }

        // Update is called once per frame
        private void Update()
        {
            isInChaseRadius = Vector3.Distance(transform.position, player.position) <= chaseRadius;
            isInAttackRadius = Vector3.Distance(transform.position, player.position) <= attackRadius;

            if (!isInAttackRadius && !isInChaseRadius) Patrol();
            else if (!isInAttackRadius && isInChaseRadius) Chase();
            else if (!alreadyAttacked) Attack();
        }

        private void Patrol()
        {
            if (agent.remainingDistance <= agent.stoppingDistance) isPatrolling = false;          
            if (isPatrolling) return;

            RaycastHit hit;
            Vector3 randomPoint;

            do
            {
                float randomX = Random.Range(-patrolRadius, patrolRadius);
                float randomZ = Random.Range(-patrolRadius, patrolRadius);
                randomPoint = new Vector3(transform.position.x + randomX, transform.position.y + transform.localScale.y,
                    transform.position.z + randomZ);
            } while (!Physics.Raycast(randomPoint, Vector3.down, out hit, 100f, NavMesh.AllAreas));

            agent.SetDestination(hit.point);
            isPatrolling = true;
        }

        private void Chase()
        {
            isPatrolling = false;
            agent.SetDestination(player.position);
            agent.speed = ChaseSpeed;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void Attack()
        {
            isPatrolling = false;
            agent.SetDestination(transform.position);
            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), attackCooldown);
            }

            Rigidbody rb =
                Instantiate(projectile, transform.position + transform.forward + transform.up, Quaternion.identity)
                    .GetComponent<Rigidbody>();

            FireBall fireball = rb.GetComponent<FireBall>();             

            float distance = Mathf.Sqrt(Mathf.Pow(player.position.x - rb.position.x, 2) + Mathf.Pow(player.position.z - rb.position.z, 2));
            float height = rb.position.y - playerData.getBottomY() - fireball.height/2;            
            
            float speedZ = distance * Mathf.Sqrt(Mathf.Abs(Physics.gravity.y / (2 * height)));            

            rb.velocity = transform.forward * speedZ;            
        }      

        private void DeleteProjectile(Rigidbody rb)
        {
            Destroy(rb);
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }
    }
}