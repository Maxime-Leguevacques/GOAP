using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToBranch_ACTION : GoToAction
{
    public override void Init(Dictionary<string, object> _blackBoard)
    {
        base.Init(_blackBoard);
        // Priority
        // Preconditions
        preconditions["BranchIsVisible"] = true;
    }

    public override void UpdateBlackBoardSuccessful(Dictionary<string, object> _blackBoard)
    {
        base.UpdateBlackBoardSuccessful(_blackBoard);
        // Effects
        _blackBoard["BranchIsInRange"] = true;
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        base.UpdatePlanBlackBoard(_blackBoard);
        // Effects
        _blackBoard["BranchIsInRange"] = true;
    }
}
