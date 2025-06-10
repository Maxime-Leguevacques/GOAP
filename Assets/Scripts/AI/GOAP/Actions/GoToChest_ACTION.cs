using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class GoToChest_ACTION : GoToAction
{
    public override void Init(Dictionary<string, object> _blackBoard)
    {
        base.Init(_blackBoard);
        // Priority
        // Preconditions
        preconditions["ChestIsInRange"] = false;
        preconditions["IsCarryingObject"] = true;
    }

    public override void Perform(GameObject _agent)
    {
        // Set chest as target first before performing
        if (!m_hasStarted)
        {
            Lumberjack_AI lumberjackAi = _agent.GetComponent<Lumberjack_AI>();
            lumberjackAi.targetGameObject = lumberjackAi.chest;
        }
        
        base.Perform(_agent);
    }

    public override void UpdateBlackBoardSuccessful(Dictionary<string, object> _blackBoard)
    {
        base.UpdateBlackBoardSuccessful(_blackBoard);
        // Effects
        _blackBoard["ChestIsInRange"] = true;
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        base.UpdatePlanBlackBoard(_blackBoard);
        // Effects
        _blackBoard["ChestIsInRange"] = true;
    }
}
