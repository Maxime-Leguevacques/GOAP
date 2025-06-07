using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreInChest_ACTION : Action
{
    #region Variables

    private bool m_hasInteracted = false;

    #endregion Variables
    
    
    protected override void Awake()
    {
        // Priority
        // Preconditions
        preconditions["HasWood"] = true;
        preconditions["ChestIsInRange"] = true;
        // Effects
        effects["HasWood"] = false;
        effects["IsWoodStored"] = true;
    }

    public override bool CheckPreconditions(GameObject _agent)
    {
        Lumberjack_AI lumberjackAi = _agent.GetComponent<Lumberjack_AI>();
        // Check if agent is carrying wood and chest is in range
        if (lumberjackAi.blackBoard["HasWood"].Equals(false) || lumberjackAi.blackBoard["ChestIsInRange"].Equals(false))
        {
            return false;
        }

        return true;
    }

    public override void Perform(GameObject _agent)
    {
        Lumberjack_AI lumberjackAi = _agent.GetComponent<Lumberjack_AI>();
        if (lumberjackAi.chest != null && !lumberjackAi.isInteracting && !m_hasInteracted)
        {
            m_hasInteracted = true;
            lumberjackAi.chest.GetComponent<Chest_SO>().Interact(_agent);
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
        m_hasInteracted = false;
    }
}
