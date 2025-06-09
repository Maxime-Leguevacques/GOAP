using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToBranch_ACTION : Action
{
    #region Variables

    private GameObject m_tree;
    
    private bool m_hasStarted = false;
    private NavMeshAgent m_navMeshAgent;
    
    #endregion Variables
    
    
    public override void Init(Dictionary<string, object> _blackBoard)
    {
        // Priority
        // Preconditions
        preconditions["BranchIsVisible"] = true;
        preconditions["IsGoingToBranch"] = false;
    }

    public override void Perform(GameObject _agent)
    {
        _agent.GetComponent<Lumberjack_AI>().blackBoard["IsGoingToBranch"] = true;
        if (!m_hasStarted)
        {
            m_navMeshAgent = _agent.GetComponent<NavMeshAgent>();
            if (m_navMeshAgent == null)
            {
                return;
            }

            m_navMeshAgent.SetDestination(_agent.GetComponent<Lumberjack_AI>().spottedBranch.transform.position);

            m_hasStarted = true;
        }
        
        // Check if agent reached destination
        if (!m_navMeshAgent.pathPending && m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            if (!m_navMeshAgent.hasPath || m_navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                state = EState.SUCCESSFUL;
            }
        }
    }

    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
    {
        // Effects
        _blackBoard["IsGoingToBranch"] = false;    // Set to false so planner doesn't loop on during execution
        _blackBoard["BranchIsInRange"] = true;
        _blackBoard["ChestIsInRange"] = false;
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        // Effects
        _blackBoard["IsGoingToBranch"] = true;
        _blackBoard["BranchIsInRange"] = true;
        _blackBoard["ChestIsInRange"] = false;
    }

    public override void Reset()
    {
        state = EState.PERFORMING;
        m_hasStarted = false;
    }
}
