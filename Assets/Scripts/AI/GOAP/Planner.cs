using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Planner : MonoBehaviour
{
    #region Variables
    
    [SerializeField, Tooltip("Max depth of the plan")] private int MAX_DEPTH = 25;
    [SerializeField] private bool m_isBackwardPlanning = false;
    
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
        if (m_isBackwardPlanning)
        {
            GenerateBackwardPlan(_blackBoard, _goals, plan);
        }
        else
        {
            GenerateForwardPlan(_blackBoard, _goals, plan);
        }
    }
    
    // Build all possible forward plans
    private void GenerateForwardPlan(Dictionary<string, object> _blackBoard, Dictionary<string, object> _goals, Queue<Action> _plan, int _depth = 0)
    {
        // Check if we are before the depth limit
        if (_depth >= MAX_DEPTH)
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
            
            // String check
            if (goal.Value is string goalStr && _blackBoard[goal.Key] is string blackBoardString)
            {
                if (blackBoardString != goalStr)
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
            action.InitForward(_blackBoard);
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
            
            // // Check if action won't undo any already satisfied goal
            Dictionary<string, object> simulatedBlackBoard = new(_blackBoard);
            action.UpdateForwardPlanBlackBoard(simulatedBlackBoard);
            bool invalidatesSatisfiedGoal = false;
            foreach (var goal in _goals)
            {
                if (_blackBoard.TryGetValue(goal.Key, out var currentValue) && currentValue.Equals(goal.Value))
                {
                    if (simulatedBlackBoard.TryGetValue(goal.Key, out var after) && !after.Equals(goal.Value))
                    {
                        invalidatesSatisfiedGoal = true;
                        break;
                    }
                }
            }
            
            if (invalidatesSatisfiedGoal)
            {
                continue;
            }

            Dictionary<string, object> recursiveBlackBoard = new Dictionary<string, object>(_blackBoard);
            Queue<Action> recursivePlan = new Queue<Action>(_plan);

            
            // Update blackboard
            action.UpdateForwardPlanBlackBoard(recursiveBlackBoard);
            
            // recursivePlan.Enqueue(clonedAction);
            recursivePlan.Enqueue(action);

            // Continue to generate plan
            GenerateForwardPlan(recursiveBlackBoard, _goals, recursivePlan, _depth + 1);
        }
    }
    
    // Build backward plan
    private void GenerateBackwardPlan(Dictionary<string, object> _blackBoard, Dictionary<string, object> _goals, Queue<Action> _plan, int _depth = 0)
    {
        if (_depth >= MAX_DEPTH)
        {
            return;
        }
        
        // No goal check since with backward planning, we know we will reach the goal
        
        // // ########## Backward plan generation ########## //
        
        // Check if the update of this action satisfies the goal. To do so, we need to set the goal to its value
        // minus one if it's an int or to it's a bool.

        Dictionary<string, object> targetGoal = new(_goals);
        
        // We now create a copy of the black board
        // We execute each possible action. For each action that modifies any of the goal,
        // we set the goal to - effect of that action
        Dictionary<string, object> bba = new(_goals);    // black board a

        Dictionary<string, object> differences = new();
        foreach (var action in m_actionList)
        {
            Dictionary<string, object> bbb = new(_goals);    // black board b
            action.UpdateBackwardPlanBlackBoard(bbb);

            foreach (var element in bbb)
            {
                if (bba.ContainsKey(element.Key))
                {
                    // If there is a difference
                    if (!bba[element.Key].Equals(element.Value))
                    {
                        // case bool
                        if (element.Value is bool)
                        {
                            differences[element.Key] = (bool)element.Value;
                        }
                        
                        // case int
                        if (element.Value is int)
                        {
                            differences[element.Key] = (int)bba[element.Key] - (int)element.Value;
                        }
                    }
                }
            }
        }
        
        // We now create a black board whose state is one step before any goal
        Dictionary<string, object> lastStepBlackBoard = new(_goals);
        HashSet<string> modifiedGoals = new();
        foreach (var goal in targetGoal)
        {
            // bool case
            if (goal.Value is bool)
            {
                lastStepBlackBoard[goal.Key] = (bool)differences[goal.Key];
                modifiedGoals.Add(goal.Key);
                break;
            }
            
            // int case
            if (goal.Value is int)
            {
                lastStepBlackBoard[goal.Key] = (int)goal.Value + (int)differences[goal.Key];
                modifiedGoals.Add(goal.Key);
                break;
            }
        }
        
        Debug.Log(lastStepBlackBoard);
        
        // Loop through each action to check if it satisfies the goal
        bool isAnyGoalSatisfied = false;
        List<Action> possibleActions = new();
        foreach (var action in m_actionList)
        {
            // Clone the last step black board because we will be passing it
            Dictionary<string, object> tempBlackBoard = new(lastStepBlackBoard);
            // Perform the update
            action.UpdateBackwardPlanBlackBoard(tempBlackBoard);
            // And check if any goal is satisfied
            foreach (var goal in targetGoal)
            {
                // Check if action is the same as the one who modified the lastStepBlackBoard
                if (modifiedGoals.Contains(goal.Key))
                {
                    if (tempBlackBoard[goal.Key].Equals(goal.Value))
                    {
                        isAnyGoalSatisfied = true;
                    }
                }
            }

            // If any goal is satisfied, add the action to a list of possible actions
            if (isAnyGoalSatisfied)
            {
                possibleActions.Add(action);
            }
        }
        
        
        // Decide which action to use here
        Action selectedAction = possibleActions[Random.Range(0, possibleActions.Count)];
        // Add the action to the plan
        Queue<Action> recursivePlan = new Queue<Action>(_plan);
        recursivePlan.Enqueue(selectedAction);

        // So the recursive goals are the last step black board + the updates,
        Dictionary<string, object> recursiveGoals = new(lastStepBlackBoard);
        selectedAction.UpdateBackwardPlanBlackBoard(recursiveGoals);
        foreach (var goal in lastStepBlackBoard)
        {
            recursiveGoals[goal.Key] = goal.Value;
        }
        
        // However we need to add the preconditions to the goal
        selectedAction.InitBackward(recursiveGoals);

        // Now that we have selected the action, do recursion with last step black board
        GenerateBackwardPlan(_blackBoard, recursiveGoals, _plan, _depth + 1);
    }
}
