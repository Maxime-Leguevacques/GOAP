using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class GoToTree_ACTION : GoToAction
{
    public override void InitForward(Dictionary<string, object> _blackBoard)
    {
        base.InitForward(_blackBoard);
        // Priority
        // Preconditions
        preconditions["TreeIsVisible"] = true;
    }

    public override void InitBackward(Dictionary<string, object> _blackBoard)
    {
        base.InitBackward(_blackBoard);
        // Priority
        // Preconditions
        _blackBoard["TreeIsVisible"] = true;
    }

    public override void UpdateBlackBoardSuccessful(Dictionary<string, object> _blackBoard)
    {
        base.UpdateBlackBoardSuccessful(_blackBoard);
        // Effects
        _blackBoard["TreeIsInRange"] = true;
    }

    public override void UpdateBlackBoardUnsuccessful(Dictionary<string, object> _blackBoard)
    {
        base.UpdateBlackBoardUnsuccessful(_blackBoard);
        // Effects
        _blackBoard["TreeIsInRange"] = true;
    }

    public override void UpdateForwardPlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        base.UpdateForwardPlanBlackBoard(_blackBoard);
        // Effects
        _blackBoard["TreeIsInRange"] = true;
    }
}
