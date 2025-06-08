using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class GoToChest_ACTION : Action
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
        // Effects
        effects["ChestIsInRange"] = true;
    }

    public override bool CheckPreconditions(GameObject _agent)
    {
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

            m_navMeshAgent.SetDestination(_agent.GetComponent<Lumberjack_AI>().chest.transform.position);

            m_hasStarted = true;
        }
        
        // Check if agent reached destination
        if (!m_navMeshAgent.pathPending && m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            if (!m_navMeshAgent.hasPath || m_navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                // Arrived
                UpdateBlackBoard(_agent.GetComponent<Lumberjack_AI>().blackBoard);
                state = EState.SUCCESSFUL;
            }
        }
    }

    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
    {
        foreach (var effect in effects)
        {
            _blackBoard[effect.Key] = effect.Value;
        }
    }

    public override void Reset()
    {
        state = EState.PERFORMING;
        m_hasStarted = false;
    }
}
