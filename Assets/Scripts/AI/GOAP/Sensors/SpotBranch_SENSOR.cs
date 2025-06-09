using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpotBranch_SENSOR : VisionsType_SENSOR
{
    #region Variables

    // ########## DEBUG ########## //
    [Header("Debug")]
    [SerializeField] private bool m_showSpotBranchRadius = false;

    #endregion Variables
    
    
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("branch") && 
            m_lumberjackAi.targetGameObject == null &&  
            m_lumberjackAi.blackBoard["IsCarryingObject"].Equals(false))
        {
            m_lumberjackAi.targetGameObject = _other.gameObject;
            m_lumberjackAi.blackBoard["BranchIsVisible"] = true;
            m_lumberjackAi.RePlan();
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("branch") && m_lumberjackAi.targetGameObject != null)
        {
            // m_lumberjackAi.spottedBranch = null;
            // m_lumberjackAi.blackBoard["BranchIsVisible"] = false;
            // m_lumberjackAi.RePlan();
        }    
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.6f, 0.3f, 0.1f); // RGB for brown
        if (m_showSpotBranchRadius)
        {
            Gizmos.DrawWireSphere(transform.position, m_visionRadius);
        }
    }
}
