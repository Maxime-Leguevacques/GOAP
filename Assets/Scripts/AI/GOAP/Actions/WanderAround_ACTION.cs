using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class WanderAround_ACTION : Action
{
    #region Variables

    private bool m_hasStarted = false;
    private NavMeshAgent m_navMeshAgent;

    [SerializeField] private float m_minWanderRadius = 25.0f;
    [SerializeField] private float m_maxWanderRadius = 40.0f;
    
    // ########## DEBUG ########## //
    [Header("Debug")]
    [SerializeField] private bool m_showWanderRadius = false;
    [SerializeField] private bool m_showDestination = false;
    
    #endregion Variables
    
    
    public override void Init(Dictionary<string, object> _blackBoard)
    {
        // Priority
        // Preconditions
        preconditions["TreeIsVisible"] = false;
        preconditions["BranchIsVisible"] = false;
        preconditions["OreIsVisible"] = false;
        preconditions["CarriedObject"] = "";
    }

    public override void Perform(GameObject _agent)
    {
        if (!m_hasStarted)
        {
            m_navMeshAgent = _agent.GetComponent<NavMeshAgent>();
            if (m_navMeshAgent == null)
            {
                return;
            }

            Lumberjack_AI lumberjackAi = _agent.GetComponent<Lumberjack_AI>();
            float radius = Random.Range(m_minWanderRadius, m_maxWanderRadius);
            Vector2 randomDir2D = Random.insideUnitCircle.normalized;
            Vector3 randomDir3D = new Vector3(randomDir2D.x, 0.0f, randomDir2D.y) * radius;
            randomDir3D += _agent.transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDir3D, out hit, radius, 1))
            {
                m_navMeshAgent.SetDestination(hit.position);
            }

            m_hasStarted = true;
        }

        // Check if agent reached destination
        if (!m_navMeshAgent.pathPending && m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            if (!m_navMeshAgent.hasPath || m_navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                // Set to unsuccessful as a trick to make planner re-perform it
                state = EState.UNSUCCESSFUL;
            }
        }
    }

    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
    {
        _blackBoard["TreeIsVisible"] = false;    // Set it to false so planner will re-perform it
        _blackBoard["BranchIsVisible"] = false;    // Set it to false so planner will re-perform it
        _blackBoard["OreIsVisible"] = false;    // Set it to false so planner will re-perform it
        _blackBoard["ChestIsInRange"] = false;
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        _blackBoard["TreeIsVisible"] = true;
        _blackBoard["BranchIsVisible"] = true;
        _blackBoard["OreIsVisible"] = true;
        _blackBoard["ChestIsInRange"] = false;
    }

    public override void Reset()
    {
        state = EState.PERFORMING;
        m_hasStarted = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (m_showWanderRadius)
        {
            Gizmos.DrawWireSphere(transform.position, m_minWanderRadius);
            Gizmos.DrawWireSphere(transform.position, m_maxWanderRadius);
            
        }
        
        if (m_showDestination)
        {
            Gizmos.DrawSphere(gameObject.GetComponent<NavMeshAgent>().destination, 1.0f);
        }
    }
}