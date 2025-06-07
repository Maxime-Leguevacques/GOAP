using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [HideInInspector] public GameObject spottedTree;
    [SerializeField] public GameObject chest;
    
    public bool isInteracting = false;

    #endregion Variables


    private void Awake()
    {
        m_planner = GetComponent<Planner>();
    }

    private void Start()
    {
        blackBoard = new()
        {
            { "TreeIsVisible", false },
            { "TreeIsInRange", false },
            { "IsGoingToTree", false },
            { "HasWood", false },
            { "ChestIsInRange", false },
            { "IsWoodStored", false }
        };
            
        m_goals = new();
        
        // Plan
        RePlan();
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
        m_goals[_newGoal.Key] = _newGoal.Value;
        RePlan();
    }

    public void RePlan()
    {
        m_currenAction = null;
        m_plannedActions.Clear();
        m_planner.Plan(blackBoard, m_goals); 
        if (m_planner.possiblePlans.Count > 0)
        {
            // Get the smallest by number of actions
            Queue<Action> smallestPlan = m_planner.possiblePlans.OrderBy(plan => plan.Count).FirstOrDefault();
            // Get the cheapest by action cost
            
            m_plannedActions = smallestPlan;

            foreach (var action in m_plannedActions)
            {
                Debug.Log(action);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, minWanderRadius);
        Gizmos.DrawWireSphere(transform.position, maxWanderRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(gameObject.GetComponent<NavMeshAgent>().destination, 1.0f);
    }
}
