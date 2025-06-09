using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpotTree_SENSOR : VisionsType_SENSOR
{
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("tree") && 
            m_lumberjackAi.spottedTree == null && 
            m_lumberjackAi.spottedBranch == null &&     // To not change path
            m_lumberjackAi.blackBoard["HasWood"].Equals(false))
        {
            m_lumberjackAi.spottedTree = _other.gameObject;
            m_lumberjackAi.blackBoard["TreeIsVisible"] = true;
            m_lumberjackAi.RePlan();
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("tree") && m_lumberjackAi.spottedTree != null)
        {
            // m_lumberjackAi.spottedTree = null;
            // m_lumberjackAi.blackBoard["TreeIsVisible"] = false;
            // m_lumberjackAi.RePlan();
        }    
    }
}
