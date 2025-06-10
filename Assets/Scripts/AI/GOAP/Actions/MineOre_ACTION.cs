using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MineOre_ACTION : Action
{
    public override void InitForward(Dictionary<string, object> _blackBoard)
    {
        // Priority
        // Preconditions
        preconditions["OreIsInRange"] = true;
        
        preconditions["IsCarryingObject"] = false;
        preconditions["CarriedObject"] = "";
    }

    public override void InitBackward(Dictionary<string, object> _blackBoard)
    {
        // Priority
        // Preconditions
        _blackBoard["OreIsInRange"] = true;
        
        _blackBoard["IsCarryingObject"] = false;
        _blackBoard["CarriedObject"] = "";
    }

    public override void Perform(GameObject _agent)
    {
        Lumberjack_AI lumberjackAi = _agent.GetComponent<Lumberjack_AI>();
        if (lumberjackAi.targetGameObject != null && !lumberjackAi.isInteracting)
        {
            if (SharedMemory.IsGameObjectAvailable(lumberjackAi.targetGameObject))
            {
                SharedMemory.objectsInInteraction.Add(lumberjackAi.targetGameObject);
                lumberjackAi.targetGameObject.GetComponent<Ore_SO>().Interact(_agent);
            }
            else
            {
                state = EState.UNSUCCESSFUL;
                return;
            }
        }
        
        if (!lumberjackAi.isInteracting)
        {
            SharedMemory.objectsInInteraction.Remove(lumberjackAi.targetGameObject);
            state = EState.SUCCESSFUL;
        }
    }

    public override void UpdateBlackBoardSuccessful(Dictionary<string, object> _blackBoard)
    {
        // Effects
        _blackBoard["OreIsVisible"] = false;
        _blackBoard["OreIsInRange"] = false;
        
        _blackBoard["IsGoingSomewhere"] = false;
        _blackBoard["IsCarryingObject"] = true;
        _blackBoard["CarriedObject"] = "ore";
    }

    public override void UpdateBlackBoardUnsuccessful(Dictionary<string, object> _blackBoard)
    {
        // Effects
        _blackBoard["OreIsVisible"] = false;
        _blackBoard["OreIsInRange"] = false;
        
        _blackBoard["IsGoingSomewhere"] = false;
        _blackBoard["IsCarryingObject"] = false;
        _blackBoard["CarriedObject"] = "";
    }

    public override void UpdateForwardPlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        // Effects
        _blackBoard["OreIsVisible"] = false;
        _blackBoard["OreIsInRange"] = false;
        
        _blackBoard["IsGoingSomewhere"] = false;
        _blackBoard["IsCarryingObject"] = true;
        _blackBoard["CarriedObject"] = "ore";
    }

    public override void UpdateBackwardPlanBlackBoard(Dictionary<string, object> _blackBoard)
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
