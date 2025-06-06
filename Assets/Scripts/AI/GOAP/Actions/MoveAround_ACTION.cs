using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround_ACTION : Action
{
    protected override void Awake()
    {
    }

    public override bool CheckPreconditions(GameObject _agent)
    {
        return true;
    }

    public override void Perform(GameObject _agent)
    {
    }

    public override void UpdateBlackBoard(Dictionary<string, object> _blackBoard)
    {
    }
}
