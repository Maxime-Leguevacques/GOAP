using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToTree_ACTION : Action
{
    #region Variables

    private GameObject m_tree;
    
    private bool m_hasStarted = false;
    private NavMeshAgent m_navMeshAgent;
    
    #endregion Variables

    
    protected override void Awake()
    {
        // Priority
        priority = 1;
        // Preconditions
        preconditions["TreeIsVisible"] = true;
        // Effects
        effects["TreeIsInRange"] = true;
    }

    public override bool CheckPreconditions(GameObject _agent)
    {
        Lumberjack_AI lumberjackAi = _agent.GetComponent<Lumberjack_AI>();
        if (!lumberjackAi.blackBoard.ContainsKey("TreeIsVisible") || lumberjackAi.blackBoard["TreeIsVisible"].Equals(false))
        {
            return false;
        }

        return true;
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

            m_navMeshAgent.SetDestination(_agent.GetComponent<Lumberjack_AI>().spottedTree.transform.position);

            m_hasStarted = true;
        }
        
        // Check if agent reached destination
        if (!m_navMeshAgent.pathPending && m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            if (!m_navMeshAgent.hasPath || m_navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                // Arrived
                UpdateBlackBoard(_agent.GetComponent<Lumberjack_AI>().blackBoard);
                // Set to unsuccessful as a trick to make planner re-perform it
                state = EState.SUCCESSFUL;
            }
        }
    }

    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
    {
        _blackBoard["TreeIsInRange"] = true;
    }

    public override void Reset()
    {
        state = EState.PERFORMING;
        m_hasStarted = false;
    }
}
