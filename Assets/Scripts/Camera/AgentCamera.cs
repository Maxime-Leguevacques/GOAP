using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCamera : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject m_agent;
    [SerializeField] private float smoothSpeed = 5f;

    private Vector3 m_offset;

    #endregion Variables
    

    void Start()
    {
        if (m_agent != null)
        {
            m_offset = transform.position - m_agent.transform.position;
        }
    }

    void LateUpdate()
    {
        if (m_agent == null) return;

        Vector3 targetPosition = new Vector3(
            m_agent.transform.position.x + m_offset.x,
            transform.position.y, // Keep Y fixed
            m_agent.transform.position.z + m_offset.z
        );

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
    
}