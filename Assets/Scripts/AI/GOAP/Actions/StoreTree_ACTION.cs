using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTree_ACTION : StoreInChestAction
{
    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
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
        
        base.UpdateBlackBoard(_blackBoard);
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
