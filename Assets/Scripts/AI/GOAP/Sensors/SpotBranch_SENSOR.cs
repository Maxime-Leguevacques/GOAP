using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpotBranch_SENSOR : VisionsType_SENSOR
{
    #region Variables

    private GameObject m_branch;
    
    // ########## DEBUG ########## //
    [Header("Debug")]
    [SerializeField] private bool m_showSpotBranchRadius = false;

    #endregion Variables
    
    
    private void OnTriggerEnter(Collider _other)
    {
        if (m_branch == null)
        {
            if (_other.CompareTag("branch"))
            {
                m_branch = _other.gameObject;

                if (m_lumberjackAi.targetGameObject == null && m_lumberjackAi.blackBoard["IsCarryingObject"].Equals(false))
                {
                    // Check if we need the wood. If so, replan
                    if (m_lumberjackAi.blackBoard["EnoughWoodStored"].Equals(false))
                    {
                        m_lumberjackAi.targetGameObject = m_branch;
                        m_lumberjackAi.RePlan();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (m_branch && _other.gameObject == m_branch)
        {
            m_branch = null;
            m_lumberjackAi.blackBoard["BranchIsVisible"] = false;
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
