using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpotTree_SENSOR : VisionsType_SENSOR
{
    #region Variables

    // ########## DEBUG ########## //
    [Header("Debug")]
    [SerializeField] private bool m_showSpotTreeRadius = false;

    #endregion Variables
    
    
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("tree") && 
            m_lumberjackAi.targetGameObject == null &&  
            m_lumberjackAi.blackBoard["IsCarryingObject"].Equals(false))
        {
            m_lumberjackAi.targetGameObject = _other.gameObject;
            m_lumberjackAi.blackBoard["TreeIsVisible"] = true;
            m_lumberjackAi.RePlan();
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("tree") && m_lumberjackAi.targetGameObject != null)
        {
            // m_lumberjackAi.spottedTree = null;
            // m_lumberjackAi.blackBoard["TreeIsVisible"] = false;
            // m_lumberjackAi.RePlan();
        }    
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (m_showSpotTreeRadius)
        {
            Gizmos.DrawWireSphere(transform.position, m_visionRadius);
        }
    }
}
