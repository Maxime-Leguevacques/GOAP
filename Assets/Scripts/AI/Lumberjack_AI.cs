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
        
        // Plan
        m_planner.Plan(m_blackBoard);
    }

    private void Update()
    {
        
    }
}
