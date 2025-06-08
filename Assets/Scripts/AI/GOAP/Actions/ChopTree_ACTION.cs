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
        // Effects
        effects["TreeIsInRange"] = false;
        effects["HasWood"] = true;
    }

    public override bool CheckPreconditions(GameObject _agent)
    {
        Lumberjack_AI lumberjackAi = _agent.GetComponent<Lumberjack_AI>();
        // Check if tree is in range and if agent isn't already carrying wood
        if (lumberjackAi.blackBoard["TreeIsInRange"].Equals(false) && lumberjackAi.blackBoard["HasWood"].Equals(true))
        {
            return false;
        }

        return true;
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
            UpdateBlackBoard(lumberjackAi.blackBoard);
            state = EState.SUCCESSFUL;
        }
    }

    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
    {
        foreach (var effect in effects)
        {
            _blackBoard[effect.Key] = effect.Value;
        }
    }

    public override void Reset()
    {
        state = EState.PERFORMING;
    }
}
