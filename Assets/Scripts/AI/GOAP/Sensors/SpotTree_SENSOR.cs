using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpotTree_SENSOR : VisionsType_SENSOR
{
    #region Variables

    private int isCollidingWithTree = 0;

    #endregion Variables


    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("tree") && m_lumberjackAi.spottedTree == null)
        {
            isCollidingWithTree++;
            m_lumberjackAi.spottedTree = _other.gameObject;

            m_lumberjackAi.blackBoard["TreeIsVisible"] = true;
            m_lumberjackAi.RePlan();
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("tree"))
        {
            isCollidingWithTree--;
        }    
    }
}
