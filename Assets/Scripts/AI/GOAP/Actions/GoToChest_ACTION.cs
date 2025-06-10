using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class GoToChest_ACTION : GoToAction
{
    public override void InitForward(Dictionary<string, object> _blackBoard)
    {
        base.InitForward(_blackBoard);
        // Priority
        // Preconditions
        preconditions["ChestIsInRange"] = false;
        preconditions["IsCarryingObject"] = true;
    }

    public override void InitBackward(Dictionary<string, object> _blackBoard)
    {
        base.InitBackward(_blackBoard);
        // Priority
        // Preconditions
        _blackBoard["ChestIsInRange"] = false;
        _blackBoard["IsCarryingObject"] = true;
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

    public override void UpdateForwardPlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        base.UpdateForwardPlanBlackBoard(_blackBoard);
        // Effects
        _blackBoard["ChestIsInRange"] = true;
    }
}
