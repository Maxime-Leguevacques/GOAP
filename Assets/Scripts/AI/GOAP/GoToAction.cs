using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class GoToAction : Action
{
    #region Variables

    private GameObject m_targetObject;
    
    protected bool m_hasStarted = false;
    private NavMeshAgent m_navMeshAgent;
    
    #endregion Variables
    
    
    public override void Init(Dictionary<string, object> _blackBoard)
    {
        // Priority
        // Preconditions
        preconditions["IsGoingSomewhere"] = false;
    }

    public override void Perform(GameObject _agent)
    {
        _agent.GetComponent<Lumberjack_AI>().blackBoard["IsHeadingSomewhere"] = true;
        if (!m_hasStarted)
        {
            m_navMeshAgent = _agent.GetComponent<NavMeshAgent>();
            if (m_navMeshAgent == null)
            {
                return;
            }

            if (_agent.GetComponent<Lumberjack_AI>().targetGameObject != null)
            {
                m_navMeshAgent.SetDestination(_agent.GetComponent<Lumberjack_AI>().targetGameObject.transform.position);
            }

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

    public override void UpdateBlackBoardSuccessful(Dictionary<string, object> _blackBoard)
    {
        _blackBoard["IsGoingSomewhere"] = false;    // Set to false so planner doesn't loop on during execution
        
        // Set everything not in range and change it in the action
        _blackBoard["TreeIsInRange"] = false;
        _blackBoard["BranchIsInRange"] = false;
        _blackBoard["OreIsInRange"] = false;
        _blackBoard["ChestIsInRange"] = false;
    }

    public override void UpdateBlackBoardUnsuccessful(Dictionary<string, object> _blackBoard)
    {
        _blackBoard["IsGoingSomewhere"] = false;    // Set to false so planner doesn't loop on during execution
        
        // Set everything not in range and change it in the action
        _blackBoard["TreeIsInRange"] = false;
        _blackBoard["BranchIsInRange"] = false;
        _blackBoard["OreIsInRange"] = false;
        _blackBoard["ChestIsInRange"] = false;
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        _blackBoard["IsGoingSomewhere"] = true;
        
        // Set everything not in range and change it in the action
        _blackBoard["TreeIsInRange"] = false;
        _blackBoard["BranchIsInRange"] = false;
        _blackBoard["OreIsInRange"] = false;
        _blackBoard["ChestIsInRange"] = false;
    }

    public override void Reset()
    {
        state = EState.PERFORMING;
        m_hasStarted = false;
    }
}
