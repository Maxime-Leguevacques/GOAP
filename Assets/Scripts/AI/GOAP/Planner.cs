using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner : MonoBehaviour
{
    #region Variables

    private List<Queue<Action>> m_possiblePlans;

    #endregion Variables
    
    
    // Return the unique planned used by the agent
    public Queue<Action> Plan(Dictionary<string, object> _goals)
    {
        m_possiblePlans.Clear();
        return null;
    }
    
    // Build all possible plans
    
}
