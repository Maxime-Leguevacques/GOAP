using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    #region Variables

    public enum EState
    {
        SLEEPING,
        PERFORMING,
        SUCCESSFUL,
        UNSUCCESSFUL
    }

    public EState state;

    public int priority = 0;

    #endregion Variables
    

    public Dictionary<string, object> preconditions = new();

    public abstract void InitForward(Dictionary<string, object> _blackBoard);
    public abstract void InitBackward(Dictionary<string, object> _blackBoard);

    public bool CheckPreconditions(GameObject _agent)
    {
        Lumberjack_AI lumberjackAi = _agent.GetComponent<Lumberjack_AI>();
        foreach (var precondition in preconditions)
        {
            if (!lumberjackAi.blackBoard[precondition.Key].Equals(precondition.Value))
            {
                return false;
            }
        }
        return true;
    }
    
    public abstract void Perform(GameObject _agent);
    public abstract void UpdateBlackBoardSuccessful(Dictionary<string, object> _blackBoard);
    public abstract void UpdateBlackBoardUnsuccessful(Dictionary<string, object> _blackBoard);
    public abstract void UpdateForwardPlanBlackBoard(Dictionary<string, object> _blackBoard);
    public abstract void UpdateBackwardPlanBlackBoard(Dictionary<string, object> _blackBoard);

    public abstract void Reset();
}
