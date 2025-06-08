using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class WanderAround_ACTION : Action
{
    #region Variables

    private bool m_hasStarted = false;
    private NavMeshAgent m_navMeshAgent;
    
    #endregion Variables
    
    
    public override void Init(Dictionary<string, object> _blackBoard)
    {
        // Priority
        // Preconditions
        preconditions["TreeIsVisible"] = false;
        preconditions["HasWood"] = false;
    }

    public override bool CheckPreconditions(GameObject _agent)
    {
        Lumberjack_AI lumberjackAi = _agent.GetComponent<Lumberjack_AI>();
        if (!lumberjackAi.blackBoard.ContainsKey("TreeIsVisible") || lumberjackAi.blackBoard["TreeIsVisible"].Equals(false))
        {
            return true;
        }

        return false;
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
            float radius = Random.Range(lumberjackAi.minWanderRadius, lumberjackAi.maxWanderRadius);
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
                // Arrived
                UpdateBlackBoard(_agent.GetComponent<Lumberjack_AI>().blackBoard);
                // Set to unsuccessful as a trick to make planner re-perform it
                state = EState.UNSUCCESSFUL;
            }
        }
    }

    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
    {
        _blackBoard["TreeIsVisible"] = false;    // Set it to false so planner will re-perform it
        _blackBoard["ChestIsInRange"] = false;
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        _blackBoard["TreeIsVisible"] = true;
        _blackBoard["ChestIsInRange"] = false;
    }

    public override void Reset()
    {
        state = EState.PERFORMING;
        m_hasStarted = false;
    }
}