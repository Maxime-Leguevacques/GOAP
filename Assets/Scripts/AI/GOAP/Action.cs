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
    public Dictionary<string, object> effects = new();

    public abstract void Init(Dictionary<string, object> _blackBoard);
    
    public abstract bool CheckPreconditions(GameObject _agent);
    public abstract void Perform(GameObject _agent);
    public abstract void UpdateBlackBoard(Dictionary<string, object> _blackBoard);

    public abstract void Reset();
}
