using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChopTree_ACTION : Action
{
    public override void Init(Dictionary<string, object> _blackBoard)
    {
        // Priority
        // Preconditions
        preconditions["TreeIsInRange"] = true;
        preconditions["HasWood"] = false;
    }

    public override void Perform(GameObject _agent)
    {
        Lumberjack_AI lumberjackAi = _agent.GetComponent<Lumberjack_AI>();
        if (lumberjackAi.spottedTree != null && !lumberjackAi.isInteracting)
        {
            lumberjackAi.spottedTree.GetComponent<Tree_SO>().Interact(_agent);
        }

        if (!lumberjackAi.isInteracting)
        {
            state = EState.SUCCESSFUL;
        }
    }

    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
    {
        // Effects
        _blackBoard["TreeIsVisible"] = false;
        _blackBoard["IsGoingToTree"] = false;
        _blackBoard["TreeIsInRange"] = false;
        _blackBoard["HasWood"] = true;
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        // Effects
        _blackBoard["TreeIsVisible"] = false;
        _blackBoard["IsGoingToTree"] = false;
        _blackBoard["TreeIsInRange"] = false;
        _blackBoard["HasWood"] = true;
    }

    public override void Reset()
    {
        state = EState.PERFORMING;
    }
}
