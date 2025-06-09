using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MineOre_ACTION : Action
{
    public override void Init(Dictionary<string, object> _blackBoard)
    {
        // Priority
        // Preconditions
        preconditions["OreIsInRange"] = true;
        
        preconditions["IsCarryingObject"] = false;
        preconditions["CarriedObject"] = "";
    }

    public override void Perform(GameObject _agent)
    {
        Lumberjack_AI lumberjackAi = _agent.GetComponent<Lumberjack_AI>();
        if (lumberjackAi.targetGameObject != null && !lumberjackAi.isInteracting)
        {
            lumberjackAi.targetGameObject.GetComponent<Ore_SO>().Interact(_agent);
        }

        if (!lumberjackAi.isInteracting)
        {
            state = EState.SUCCESSFUL;
        }
    }

    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
    {
        // Effects
        _blackBoard["OreIsVisible"] = false;
        _blackBoard["OreIsInRange"] = false;
        
        _blackBoard["IsGoingSomewhere"] = false;
        _blackBoard["IsCarryingObject"] = true;
        _blackBoard["CarriedObject"] = "ore";
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        // Effects
        _blackBoard["OreIsVisible"] = false;
        _blackBoard["OreIsInRange"] = false;
        
        _blackBoard["IsGoingSomewhere"] = false;
        _blackBoard["IsCarryingObject"] = true;
        _blackBoard["CarriedObject"] = "ore";
    }

    public override void Reset()
    {
        state = EState.PERFORMING;
    }
}
