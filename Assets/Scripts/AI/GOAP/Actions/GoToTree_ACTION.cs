using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class GoToTree_ACTION : GoToAction
{
    public override void Init(Dictionary<string, object> _blackBoard)
    {
        base.Init(_blackBoard);
        // Priority
        // Preconditions
        preconditions["TreeIsVisible"] = true;
    }

    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
    {
        base.UpdateBlackBoard(_blackBoard);
        // Effects
        _blackBoard["TreeIsInRange"] = true;
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        base.UpdatePlanBlackBoard(_blackBoard);
        // Effects
        _blackBoard["TreeIsInRange"] = true;
    }
}
