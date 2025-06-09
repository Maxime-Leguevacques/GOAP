using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpotTree_SENSOR : VisionsType_SENSOR
{
    #region Variables

    private GameObject m_tree;
    
    // ########## DEBUG ########## //
    [Header("Debug")]
    [SerializeField] private bool m_showSpotTreeRadius = false;

    #endregion Variables
    
    
    private void OnTriggerEnter(Collider _other)
    {
        if (m_tree == null)
        {
            if (_other.CompareTag("tree"))
            {
                m_tree = _other.gameObject;
                m_lumberjackAi.blackBoard["TreeIsVisible"] = true;

                if (m_lumberjackAi.targetGameObject == null && m_lumberjackAi.blackBoard["IsCarryingObject"].Equals(false))
                {
                    // Check if we need the wood. If so, replan
                    // if (m_lumberjackAi.blackBoard["EnoughWoodStored"].Equals(false))
                    {
                        m_lumberjackAi.targetGameObject = m_tree;
                        m_lumberjackAi.RePlan();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (m_tree && _other.gameObject == m_tree)
        {
            m_tree = null;
            m_lumberjackAi.blackBoard["TreeIsVisible"] = false;
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
