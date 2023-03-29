using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float chaseRadius = 10f;
    public float patrolRadius = 30f;

    bool isInChaseRadius = false;
    bool isPatrolling = false;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(GameObject.Find("Player").transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        isInChaseRadius = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) <= chaseRadius;

        if (isInChaseRadius) {
            Chase();
        } else {
            Patrol();
        }
    }

    void Patrol() {
        if (agent.remainingDistance <= agent.stoppingDistance) {
            isPatrolling = false;
        }

        if (!isPatrolling) {
            RaycastHit hit;
            Vector3 randomPoint;

            do {
                float randomX = Random.Range(-patrolRadius, patrolRadius);
                float randomZ = Random.Range(-patrolRadius, patrolRadius);
                randomPoint = new Vector3(transform.position.x + randomX, transform.position.y + transform.localScale.y, transform.position.z + randomZ);
            } while(!Physics.Raycast(randomPoint, Vector3.down, out hit, 100f, NavMesh.AllAreas));

            agent.SetDestination(hit.point);
            isPatrolling = true;
        }
    }

    void Chase() {
        isPatrolling = false;
        agent.SetDestination(GameObject.Find("Player").transform.position);
    }
}
