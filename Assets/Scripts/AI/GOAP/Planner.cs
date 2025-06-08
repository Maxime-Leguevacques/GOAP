using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner : MonoBehaviour
{
    #region Variables
    
    [SerializeField, Tooltip("Max depth of the plan")] private int MAX_DEPTH = 25;
    
    private List<Action> m_actionList;
    
    public List<Queue<Action>> possiblePlans;

    #endregion Variables


    private void Awake()
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
            return;    
        }
        
        // Check if all goals are met
        bool areGoalsMet = true;
        foreach (var goal in _goals)
        {
            // Bool check
            if (goal.Value is bool && !_blackBoard[goal.Key].Equals(goal.Value))
            {
                areGoalsMet = false;
                break;
            }
            
            // Int check
            if (goal.Value is int goalInt && _blackBoard[goal.Key] is int blackBoardInt)
            {
                if (blackBoardInt < goalInt)
                {
                    areGoalsMet = false;
                    break;
                }
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
            action.Init(_blackBoard);
            bool arePreconditionsMet = true;
            // Check if blackboard meets all preconditions
            foreach (var precondition in action.preconditions)
            {
                if (!_blackBoard[precondition.Key].Equals(precondition.Value))
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

            // Clone action to be able to reuse the same action multiple times
            // Action clonedAction = Instantiate(action);
            // clonedAction.Init(recursiveBlackBoard);
            
            // Update blackboard
            // clonedAction.UpdatePlanBlackBoard(recursiveBlackBoard);
            action.UpdatePlanBlackBoard(recursiveBlackBoard);
            
            // recursivePlan.Enqueue(clonedAction);
            recursivePlan.Enqueue(action);

            // Continue to generate plan
            GeneratePlan(recursiveBlackBoard, _goals, recursivePlan, _depth + 1);
        }
    }
}
