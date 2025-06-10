using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBranch_ACTION : StoreInChestAction
{
    public override void InitForward(Dictionary<string, object> _blackBoard)
    {
        base.InitForward(_blackBoard);

        preconditions["CarriedObject"] = "branch";
    }

    public override void InitBackward(Dictionary<string, object> _blackBoard)
    {
        base.InitBackward(_blackBoard);

        _blackBoard["CarriedObject"] = "branch";
    }

    public override void UpdateBlackBoardSuccessful(Dictionary<string, object> _blackBoard)
    {
        // Update storage
        if (_blackBoard["CarriedObject"] != null)
        {
            // tree
            if (_blackBoard["CarriedObject"].Equals("branch"))
            {
                _blackBoard["WoodStored"] = (int)_blackBoard["WoodStored"] + 1;
            }
        }
        
        base.UpdateBlackBoardSuccessful(_blackBoard);
    }

    public override void UpdateForwardPlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        if (_blackBoard["CarriedObject"] != null)
        {
            if (_blackBoard["CarriedObject"].Equals("branch"))
            {
                _blackBoard["WoodStored"] = (int)_blackBoard["WoodStored"] + 1;
            }
        }
        
        base.UpdateForwardPlanBlackBoard(_blackBoard);
    }

    public override void UpdateBackwardPlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        _blackBoard["WoodStored"] = (int)_blackBoard["WoodStored"] + 1;
        
        base.UpdateBackwardPlanBlackBoard(_blackBoard);
    }
}
