using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class Enemy : MonoBehaviour
{
    [BoxGroup("Enemy")]
    [SerializeField] float attackRange;

    [BoxGroup("Animation")]
    [SerializeField] string movingBoolParameter;

    NavMeshAgent agent;
    Animator animator;
    bool hit;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!agent.isOnNavMesh)
        {
            return;
        }

        if(Vector3.Distance(transform.position, PlayerHandler.Player.position) < attackRange)
        {
            agent.SetDestination(PlayerHandler.Player.position);
            animator.SetBool(movingBoolParameter, true);
        }
        else
        {
            agent.SetDestination(transform.position);
            animator.SetBool(movingBoolParameter, false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
