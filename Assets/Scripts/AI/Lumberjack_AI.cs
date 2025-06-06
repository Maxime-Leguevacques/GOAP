using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Planner))]
public class Lumberjack_AI : MonoBehaviour
{
    #region Variables

    private Planner m_planner;
    private Queue<Action> m_plannedActions = new();
    private Action m_currenAction;

    private Dictionary<string, object> m_blackBoard;
    private Dictionary<string, object> m_goals;

    #endregion Variables


    private void Awake()
    {
        m_planner = GetComponent<Planner>();
    }

    private void Start()
    {
        m_blackBoard = new();
        m_goals = new();
        
        // // Plan
        // m_planner.Plan(m_blackBoard, m_goals);
        //
        // m_plannedActions = m_planner.possiblePlans[0];
    }

    private void Update()
    {
        foreach (var goal in m_goals)
        {
            Debug.Log(goal.Key);
        }
        
        if (m_currenAction == null && m_plannedActions != null && m_plannedActions.Count > 0)
        {
            m_currenAction = m_plannedActions.Dequeue();

            if (!m_currenAction.CheckPreconditions(gameObject))
            {
                m_currenAction = null;
                m_plannedActions.Clear();
                m_planner.Plan(m_blackBoard, m_goals);
                if (m_planner.possiblePlans.Count > 0)
                {
                    m_plannedActions = m_planner.possiblePlans[0];
                }
                return; 
            }
        }

        if (m_currenAction != null)
        {
            if (m_currenAction.state == Action.EState.PERFORMING)
            {
                m_currenAction.Perform(gameObject);    
            }
            
            else if (m_currenAction.state == Action.EState.SUCCESSFUL)
            {
                // Update black board
                foreach (var effect in m_currenAction.effects)
                {
                    m_blackBoard[effect.Key] = effect.Value;
                }
                
                m_currenAction = null;
                
                // Check if goals are reached
                if (CheckIfGoalsAreReached(m_blackBoard, m_goals))
                {
                    Debug.Log("Goals Reached !");
                }
            }

            else if (m_currenAction.state == Action.EState.UNSUCCESSFUL)
            {
                m_currenAction = null;
                m_planner.Plan(m_blackBoard, m_goals);

                if (m_plannedActions != null && m_plannedActions.Count == 0)
                {
                    m_plannedActions.Clear();
                }
                
                if (m_planner.possiblePlans.Count > 0)
                {
                    m_plannedActions = m_planner.possiblePlans[0];
                }
                return; 
            }
        }
    }

    private bool CheckIfGoalsAreReached(Dictionary<string, object> _blackBoard, Dictionary<string, object> _goals)
    {
        foreach (var goal in _goals)
        {
            if (!_blackBoard.ContainsKey(goal.Key) || !_blackBoard[goal.Key].Equals(goal.Value))
            {
                return false;
            }
        }

        return true;
    }

    public void AddGoal(KeyValuePair<string, object> _newGoal)
    {
        m_goals.Add(_newGoal.Key, _newGoal.Value);
    }
}
