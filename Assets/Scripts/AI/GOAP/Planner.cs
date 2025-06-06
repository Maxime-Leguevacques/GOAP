using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner : MonoBehaviour
{
    #region Variables

    [SerializeField] private const int MAX_DEPTH = 25;
    
    private List<Action> m_actionList;
    
    public List<Queue<Action>> possiblePlans;

    #endregion Variables


    private void Start()
    {
        // Load actions
        m_actionList = new List<Action>(GetComponents<Action>());
        possiblePlans = new List<Queue<Action>>();
    }

    // Return the unique planned used by the agent
    public void Plan(Dictionary<string, object> _blackBoard, Dictionary<string, object> _goals)
    {
        possiblePlans.Clear();
        
        Queue<Action> plan = new();
        GeneratePlan(_blackBoard, _goals, plan);
    }
    
    // Build all possible plans
    private void GeneratePlan(Dictionary<string, object> _blackBoard, Dictionary<string, object> _goals, Queue<Action> _plan, int _depth = 0)
    {
        // Check if we are before the depth limit
        if (_depth > MAX_DEPTH)
        {
            Debug.Log("MAX DEPTH REACHED");
            foreach (var action in _plan)
            {
                Debug.Log(action);
            }
            return;    
        }
        
        // Check if all goals are met
        bool areGoalsMet = true;
        foreach (var goal in _goals)
        {
            if (!_blackBoard.ContainsKey(goal.Key) || !_blackBoard[goal.Key].Equals(goal.Value))
            {
                areGoalsMet = false;
                break;
            }
        }

        // If so, return the plan
        if (areGoalsMet)
        {
            possiblePlans.Add(_plan);
            return;
        }
        
        // ########## Forward plan generation ########## //
        
        // Loop through all actions
        foreach (var action in m_actionList)
        {
            bool arePreconditionsMet = true;
            // Check if blackboard meets all preconditions
            foreach (var precondition in action.preconditions)
            {
                if (!_blackBoard.ContainsKey(precondition.Key) || !_blackBoard[precondition.Key].Equals(precondition.Value))
                {
                    arePreconditionsMet = false;
                    break;
                }
            }

            // Skip if preconditions aren't met
            if (!arePreconditionsMet)
            {
                continue;
            }

            Dictionary<string, object> recursiveBlackBoard = new Dictionary<string, object>(_blackBoard);
            Queue<Action> recursivePlan = new Queue<Action>(_plan);
            
            // Update blackboard
            foreach (var effect in action.effects)
            {
                recursiveBlackBoard[effect.Key] = effect.Value;
            }
            recursivePlan.Enqueue(action);

            // Continue to generate plan
            GeneratePlan(recursiveBlackBoard, _goals, recursivePlan, _depth + 1);
        }
    }
}
