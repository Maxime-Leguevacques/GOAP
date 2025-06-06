using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private List<Action> m_actions;

    [Header("Goal UI")]
    [SerializeField] private GameObject m_addGoalScrollbar;
    [SerializeField] private GameObject m_addGoalTemplateButton;
    

    #endregion Variables


    private void Awake()
    {
        m_actions = new List<Action>(GetComponents<Action>());

        for (int i = 0; i < m_actions.Count; i++)
        {
            string name = m_actions[i].GetType().Name;
            name = name.Replace("_ACTION", "");
            CreateGoalButton(name);
        }
    }

    private void CreateGoalButton(string _goalName)
    {
        GameObject goalButtonGO = Instantiate(m_addGoalTemplateButton, m_addGoalScrollbar.transform);
        AddGoalTemplateButton buttonScript = goalButtonGO.GetComponent<AddGoalTemplateButton>();
        buttonScript.SetText(_goalName);
        
        // Add click behavior
    }
}
