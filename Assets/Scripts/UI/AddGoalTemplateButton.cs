using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddGoalTemplateButton : MonoBehaviour
{
    #region Variables

    [SerializeField] private TMP_Text m_text;

    #endregion Variables
    
    // Start is called before the first frame update
    public void SetText(string _value)
    {
        if (m_text != null)
        {
            m_text.text = _value;
        }
    }
}
