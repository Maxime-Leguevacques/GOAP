using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToBranch_ACTION : GoToAction
{
    public override void InitForward(Dictionary<string, object> _blackBoard)
    {
        base.InitForward(_blackBoard);
        // Priority
        // Preconditions
        preconditions["BranchIsVisible"] = true;
    }

    public override void InitBackward(Dictionary<string, object> _blackBoard)
    {
        base.InitBackward(_blackBoard);
        // Priority
        // Preconditions
        _blackBoard["BranchIsVisible"] = true;
    }

    public override void UpdateBlackBoardSuccessful(Dictionary<string, object> _blackBoard)
    {
        base.UpdateBlackBoardSuccessful(_blackBoard);
        // Effects
        _blackBoard["BranchIsInRange"] = true;
    }

    public override void UpdateForwardPlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        base.UpdateForwardPlanBlackBoard(_blackBoard);
        // Effects
        _blackBoard["BranchIsInRange"] = true;
    }

    public override void UpdateBackwardPlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        base.UpdateForwardPlanBlackBoard(_blackBoard);
        // Effects
        _blackBoard["BranchIsInRange"] = true;
    }
}
