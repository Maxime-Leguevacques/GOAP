using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddGoalTemplateButton : MonoBehaviour
{
    #region Variables

    [SerializeField] private TMP_Text m_text;
    private UiManager m_uiManager;

    #endregion Variables


    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Start is called before the first frame update
    public void SetText(string _value)
    {
        if (m_text != null)
        {
            m_text.text = _value;
        }
    }

    public void SetUiManager(UiManager _uiManager)
    {
        m_uiManager = _uiManager;
    }

    private void OnClick()
    {
        m_uiManager?.CreateGoalInstance(m_text.text);
    }
}
