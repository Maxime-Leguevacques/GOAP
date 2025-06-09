using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpotBranch_SENSOR : VisionsType_SENSOR
{
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("branch") && 
            m_lumberjackAi.spottedBranch == null && 
            m_lumberjackAi.spottedTree == null &&     // To not change path
            m_lumberjackAi.blackBoard["HasWood"].Equals(false))
        {
            m_lumberjackAi.spottedBranch = _other.gameObject;
            m_lumberjackAi.blackBoard["BranchIsVisible"] = true;
            m_lumberjackAi.RePlan();
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("branch") && m_lumberjackAi.spottedBranch != null)
        {
            // m_lumberjackAi.spottedBranch = null;
            // m_lumberjackAi.blackBoard["BranchIsVisible"] = false;
            // m_lumberjackAi.RePlan();
        }    
    }
}
