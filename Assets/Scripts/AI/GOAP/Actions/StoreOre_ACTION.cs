using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreOre_ACTION : StoreInChestAction
{
    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
    {
        // Update storage
        if (_blackBoard["CarriedObject"] != null)
        {
            // tree
            if (_blackBoard["CarriedObject"].Equals("ore"))
            {
                _blackBoard["OreStored"] = (int)_blackBoard["OreStored"] + 1;
            }
        }
        
        base.UpdateBlackBoard(_blackBoard);
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        if (_blackBoard["CarriedObject"] != null)
        {
            // tree
            if (_blackBoard["CarriedObject"].Equals("ore"))
            {
                _blackBoard["OreStored"] = (int)_blackBoard["OreStored"] + 1;
            }
        }
        
        base.UpdatePlanBlackBoard(_blackBoard);
    }
}
