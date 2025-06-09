using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpotOre_SENSOR : VisionsType_SENSOR
{
    #region Variables

    private GameObject m_ore;
    
    // ########## DEBUG ########## //
    [Header("Debug")]
    [SerializeField] private bool m_showSpotOreRadius = false;

    #endregion Variables

    private void Start()
    {
        Debug.Log("IS INITIALIZED");
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (m_ore == null)
        {
            if (_other.CompareTag("ore"))
            {
                m_ore = _other.gameObject;
                m_lumberjackAi.blackBoard["OreIsVisible"] = true;

                if (m_lumberjackAi.targetGameObject == null && m_lumberjackAi.blackBoard["IsCarryingObject"].Equals(false))
                {
                    // Check if we need the wood. If so, replan
                    if (m_lumberjackAi.blackBoard["EnoughWoodStored"].Equals(false))
                    {
                        m_lumberjackAi.targetGameObject = m_ore;
                        m_lumberjackAi.RePlan();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (m_ore && _other.gameObject == m_ore)
        {
            m_ore = null;
            m_lumberjackAi.blackBoard["OreIsVisible"] = false;
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
