using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Planner))]
public class Lumberjack_AI : MonoBehaviour
{
    #region Variables

    // ########## PRIVATE ########## //
    private Planner m_planner;
    private Queue<Action> m_plannedActions = new();
    private Action m_currenAction;

    private Dictionary<string, object> m_goals;
    
    // ########## PUBLIC ########## //
    [SerializeField] public float  minWanderRadius = 25.0f;
    [SerializeField] public float  maxWanderRadius = 40.0f;
    
    public Dictionary<string, object> blackBoard;

    public GameObject spottedTree;

    #endregion Variables


    private void Awake()
    {
        m_planner = GetComponent<Planner>();
    }

    private void Start()
    {
        blackBoard = new()
        {
            { "TreeIsVisible", false }
        };
            
        m_goals = new();
        
        // Plan
        m_planner.Plan(blackBoard, m_goals);
        m_plannedActions = m_planner.possiblePlans[0];
    }

    private void Update()
    {
        // Update plan
        if (m_currenAction == null && m_plannedActions != null && m_plannedActions.Count > 0)
        {
            m_currenAction = m_plannedActions.Dequeue();

            m_currenAction.Reset();

            if (!m_currenAction.CheckPreconditions(gameObject))
            {
                RePlan();
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
                    blackBoard[effect.Key] = effect.Value;
                }
                
                m_currenAction = null;
                
                // Check if goals are reached
                if (CheckIfGoalsAreReached(blackBoard, m_goals))
                {
                    Debug.Log("Goals Reached !");
                }
            }

            else if (m_currenAction.state == Action.EState.UNSUCCESSFUL)
            {
                m_currenAction.UpdateBlackBoard(blackBoard);
                
                RePlan();
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
        if (!m_goals.ContainsKey(_newGoal.Key))
        {
            m_goals.Add(_newGoal.Key, _newGoal.Value);
        }
        else
        {
            m_goals[_newGoal.Key] = _newGoal.Value;
        }
        
        // Plan
        m_planner.Plan(blackBoard, m_goals);
        if (m_planner.possiblePlans.Count > 0)
        {
            m_plannedActions = m_planner.possiblePlans[0];
        }
        else
        {
            Debug.LogWarning("No plan could be generated for the given goals.");
            m_plannedActions.Clear();
        }
    }

    public void RePlan()
    {
        m_currenAction = null;
        m_plannedActions.Clear();
        m_planner.Plan(blackBoard, m_goals); 
        if (m_planner.possiblePlans.Count > 0)
        {
            m_plannedActions = m_planner.possiblePlans[0];
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, minWanderRadius);
        Gizmos.DrawWireSphere(transform.position, maxWanderRadius);
        
        Gizmos.DrawSphere(gameObject.GetComponent<NavMeshAgent>().destination, 1.0f);
    }
}
