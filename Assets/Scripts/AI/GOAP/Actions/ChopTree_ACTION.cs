using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTree_ACTION : Action
{
    protected override void Awake()
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
        // Check if tree is in range
        if (!lumberjackAi.blackBoard.ContainsKey("TreeIsInRange") || lumberjackAi.blackBoard["TreeIsInRange"].Equals(false))
        {
            return false;
        }
        // Check if we aren't already carrying wood
        if (lumberjackAi.blackBoard.ContainsKey("HasWood") && lumberjackAi.blackBoard["HasWood"].Equals(true))
        {
            return false;
        }

        return true;
    }

    public override void Perform(GameObject _agent)
    {
        Lumberjack_AI lumberjackAi = _agent.GetComponent<Lumberjack_AI>();
        if (lumberjackAi.spottedTree != null)
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
