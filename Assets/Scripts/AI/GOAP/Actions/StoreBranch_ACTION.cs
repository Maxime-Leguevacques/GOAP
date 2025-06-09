using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBranch_ACTION : StoreInChestAction
{
    public override void Init(Dictionary<string, object> _blackBoard)
    {
        base.Init(_blackBoard);

        preconditions["CarriedObject"] = "branch";
    }

    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
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
        
        base.UpdateBlackBoard(_blackBoard);
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        if (_blackBoard["CarriedObject"] != null)
        {
            // tree
            if (_blackBoard["CarriedObject"].Equals("branch"))
            {
                _blackBoard["WoodStored"] = (int)_blackBoard["WoodStored"] + 1;
            }
        }
        
        base.UpdatePlanBlackBoard(_blackBoard);
    }
}
