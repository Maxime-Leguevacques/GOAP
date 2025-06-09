using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToOre_ACTION : GoToAction
{
    public override void Init(Dictionary<string, object> _blackBoard)
    {
        base.Init(_blackBoard);
        // Priority
        // Preconditions
        preconditions["OreIsVisible"] = true;
    }
    
    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
    {
        base.UpdateBlackBoard(_blackBoard);
        // Effects
        _blackBoard["OreIsInRange"] = true;
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        base.UpdatePlanBlackBoard(_blackBoard);
        // Effects
        _blackBoard["OreIsInRange"] = true;
    }
}
