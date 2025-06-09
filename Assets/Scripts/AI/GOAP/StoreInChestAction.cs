using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreInChestAction : Action
{
    #region Variables

    private bool m_hasInteracted = false;

    #endregion Variables
    
    
    public override void Init(Dictionary<string, object> _blackBoard)
    {
        // Priority
        // Preconditions
        preconditions["IsCarryingObject"] = true;
        preconditions["ChestIsInRange"] = true;
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
            state = EState.SUCCESSFUL;
        }
    }

    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
    {
        // Effects
        _blackBoard["IsGoingSomewhere"] = false;
        _blackBoard["CarriedObject"] = "";
        _blackBoard["IsCarryingObject"] = false;
    }

    public override void UpdatePlanBlackBoard(Dictionary<string, object> _blackBoard)
    {
        // Effects
        _blackBoard["IsGoingSomewhere"] = false;
        _blackBoard["CarriedObject"] = "";
        _blackBoard["IsCarryingObject"] = false;
    }

    public override void Reset()
    {
        state = EState.PERFORMING;
        m_hasInteracted = false;
    }
}
