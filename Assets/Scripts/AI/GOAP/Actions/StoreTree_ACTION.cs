using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTree_ACTION : StoreInChestAction
{
    public override void Init(Dictionary<string, object> _blackBoard)
    {
        base.Init(_blackBoard);

        preconditions["CarriedObject"] = "tree";
    }
    
    public override void UpdateBlackBoardSuccessful(Dictionary<string, object> _blackBoard)
    {
        // Update storage
        if (_blackBoard["CarriedObject"] != null)
        {
            // tree
            if (_blackBoard["CarriedObject"].Equals("tree"))
            {
                _blackBoard["WoodStored"] = (int)_blackBoard["WoodStored"] + 2;
            }
        }
        
        base.UpdateBlackBoardSuccessful(_blackBoard);
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        if (_blackBoard["CarriedObject"] != null)
        {
            // tree
            if (_blackBoard["CarriedObject"].Equals("tree"))
            {
                _blackBoard["WoodStored"] = (int)_blackBoard["WoodStored"] + 2;
            }
        }
        
        base.UpdatePlanBlackBoard(_blackBoard);
    }
}
