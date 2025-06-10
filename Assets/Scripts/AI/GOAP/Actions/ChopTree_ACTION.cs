using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChopTree_ACTION : Action
{
    public override void InitForward(Dictionary<string, object> _blackBoard)
    {
        // Priority
        // Preconditions
        preconditions["TreeIsInRange"] = true;
        
        preconditions["IsCarryingObject"] = false;
        preconditions["CarriedObject"] = "";
    }

    public override void InitBackward(Dictionary<string, object> _blackBoard)
    {
        // Priority
        // Preconditions
        _blackBoard["TreeIsInRange"] = true;
        
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
                // Check if no agent is already cutting the tree
                SharedMemory.objectsInInteraction.Add(lumberjackAi.targetGameObject);
                lumberjackAi.targetGameObject.GetComponent<Tree_SO>().Interact(_agent);
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
        _blackBoard["TreeIsVisible"] = false;
        _blackBoard["TreeIsInRange"] = false;
        
        _blackBoard["IsGoingSomewhere"] = false;
        _blackBoard["IsCarryingObject"] = true;
        _blackBoard["CarriedObject"] = "tree";
    }

    public override void UpdateBlackBoardUnsuccessful(Dictionary<string, object> _blackBoard)
    {
        // Effects
        _blackBoard["TreeIsVisible"] = false;
        _blackBoard["TreeIsInRange"] = false;
        
        _blackBoard["IsGoingSomewhere"] = false;
        _blackBoard["IsCarryingObject"] = false;
        _blackBoard["CarriedObject"] = "";
    }

    public override void UpdateForwardPlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        // Effects
        _blackBoard["TreeIsVisible"] = false;
        _blackBoard["TreeIsInRange"] = false;
        
        _blackBoard["IsGoingSomewhere"] = false;
        _blackBoard["IsCarryingObject"] = true;
        _blackBoard["CarriedObject"] = "tree";
    }

    public override void UpdateBackwardPlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        _blackBoard["IsGoingSomewhere"] = false;
        _blackBoard["IsCarryingObject"] = true;
        _blackBoard["CarriedObject"] = "tree";
    }

    public override void Reset()
    {
        state = EState.PERFORMING;
    }
}
