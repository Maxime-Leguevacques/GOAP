using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotOre_SENSOR : VisionsType_SENSOR
{
    #region Variables

    // ########## DEBUG ########## //
    [Header("Debug")]
    [SerializeField] private bool m_showSpotOreRadius = false;

    #endregion Variables
    
    
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("ore") && 
            m_lumberjackAi.targetGameObject == null &&  
            m_lumberjackAi.blackBoard["IsCarryingObject"].Equals(false))
        {
            m_lumberjackAi.targetGameObject = _other.gameObject;
            m_lumberjackAi.blackBoard["OreIsVisible"] = true;
            m_lumberjackAi.RePlan();
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("ore") && m_lumberjackAi.targetGameObject != null)
        {
            // m_lumberjackAi.spottedTree = null;
            // m_lumberjackAi.blackBoard["TreeIsVisible"] = false;
            // m_lumberjackAi.RePlan();
        }    
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 0.84f, 0.0f); // Classic gold
        if (m_showSpotOreRadius)
        {
            Gizmos.DrawWireSphere(transform.position, m_visionRadius);
        }
    }
}
