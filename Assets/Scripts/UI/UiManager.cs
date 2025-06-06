using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private List<Action> m_actions;

    [SerializeField] private GameObject m_agent;
    
    [Header("Goal UI")]
    [SerializeField] private GameObject m_addGoalVLG;
    [SerializeField] private GameObject m_addGoalTemplateButton;
    [SerializeField] private GameObject m_goalsVLG;
    [SerializeField] private GameObject m_goalTemplateElement;

    #endregion Variables


    private void Start()
    {
        m_actions = new List<Action>(GetComponents<Action>());

        for (int i = 0; i < m_actions.Count; i++)
        {
            foreach (var effect in m_actions[i].effects)
            {
                string goalName = effect.Key;
                CreateGoalButton(goalName);
            }
        }
    }

    private void CreateGoalButton(string _goalName)
    {
        GameObject goalButtonGO = Instantiate(m_addGoalTemplateButton, m_addGoalVLG.transform);
        AddGoalTemplateButton buttonScript = goalButtonGO.GetComponent<AddGoalTemplateButton>();
        buttonScript.SetText(_goalName);
        buttonScript.SetUiManager(this);
    }

    public void CreateGoalInstance(string _goalName)
    {
        GameObject goalGO = Instantiate(m_goalTemplateElement, m_goalsVLG.transform);
        goalGO.GetComponentInChildren<TMP_Text>().text = _goalName;

        m_agent.GetComponent<Lumberjack_AI>().AddGoal(new KeyValuePair<string, object>(_goalName, true));
    }
}
