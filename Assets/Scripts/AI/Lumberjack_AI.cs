using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Planner))]
[RequireComponent(typeof(AudioSource))]
public class Lumberjack_AI : MonoBehaviour
{
    #region Variables

    // ########## PRIVATE ########## //
    private Planner m_planner;
    private Queue<Action> m_plannedActions = new();
    private Action m_currentAction;

    private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_noPlanFoundClip;
    [SerializeField] private AudioClip m_goalsReachedClip;

    private Dictionary<string, object> m_goals;
    
    // ########## PUBLIC ########## //
    
    public Dictionary<string, object> blackBoard;

    [HideInInspector] public GameObject targetGameObject;
    [SerializeField] public GameObject chest;
    
    public bool isInteracting = false;

    #endregion Variables


    private void Awake()
    {
        m_planner = GetComponent<Planner>();
        m_audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (!chest)
        {
            Debug.LogError("ERROR: No chest given"); 
            return;
        }
        
        
        blackBoard = new()
        {
            { "TreeIsVisible", false },
            { "TreeIsInRange", false },
            
            { "BranchIsVisible", false },
            { "BranchIsInRange", false },
            
            { "OreIsVisible", false },
            { "OreIsInRange", false },
            
            { "ChestIsInRange", false },
            
            { "IsCarryingObject", false},
            { "CarriedObject", "" },
            
            { "IsGoingSomewhere", false },
            
            { "WoodStored", 0 },
            { "OreStored", 0 },
            
            { "EnoughWoodStored", false },
            { "EnoughOreStored", false }
        };

        m_goals = new()
        {
            { "WoodStored", 3 },
            { "OreStored", 3 }
        };
        
        // Plan
        RePlan();
    }

    private void Update()
    {
        // Debug.Log(blackBoard);
        // Update plan
        if (m_currentAction == null && m_plannedActions != null && m_plannedActions.Count > 0)
        {
            m_currentAction = m_plannedActions.Dequeue();

            m_currentAction.Reset();

            if (!m_currentAction.CheckPreconditions(gameObject))
            {
                RePlan();
                return; 
            }
        }

        if (m_currentAction != null)
        {
            if (m_currentAction.state == Action.EState.PERFORMING)
            {
                m_currentAction.Perform(gameObject);    
            }
            
            else if (m_currentAction.state == Action.EState.SUCCESSFUL)
            {
                // Update plan black board
                m_currentAction.UpdateBlackBoardSuccessful(blackBoard);
                
                m_currentAction.Reset();
                m_currentAction = null;

                if ((int)blackBoard["WoodStored"] >= (int)m_goals["WoodStored"])
                {
                    blackBoard["EnoughWoodStored"] = true;
                }
                
                if ((int)blackBoard["OreStored"] >= (int)m_goals["OreStored"])
                {
                    blackBoard["EnoughOreStored"] = true;
                }
                
                // Check if goals are reached
                if (CheckIfGoalsAreReached(blackBoard, m_goals))
                {
                    Debug.Log("Goals Reached !");
                    GetComponent<NavMeshAgent>().isStopped = true;
                    m_audioSource.PlayOneShot(m_goalsReachedClip);
                }
                else
                {
                    Debug.Log("Goals Not Reached, Replanning !");
                    RePlan();
                }
                
            }

            else if (m_currentAction.state == Action.EState.UNSUCCESSFUL)
            {
                m_currentAction.UpdateBlackBoardUnsuccessful(blackBoard);
                m_currentAction.Reset();
                m_currentAction = null;
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
        Debug.Log("Replanning");
        m_currentAction = null;
        m_plannedActions.Clear();
        m_planner.Plan(blackBoard, m_goals); 
        if (m_planner.possiblePlans.Count > 0)
        {
            // Get the smallest by number of actions
            Queue<Action> smallestPlan = m_planner.possiblePlans.OrderBy(plan => plan.Count).FirstOrDefault();
            Debug.Log("smallest plan is: " + smallestPlan.Count);
            // Get the cheapest by action cost
            
            m_plannedActions = smallestPlan;

            foreach (var action in m_plannedActions)
            {
                // Debug.Log(action);
            }
        }
        else
        {
            m_audioSource.PlayOneShot(m_noPlanFoundClip);
        }
    }
}
