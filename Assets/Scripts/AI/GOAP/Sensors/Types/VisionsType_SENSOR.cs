using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public abstract class VisionsType_SENSOR : Sensor
{
    #region Variables

    protected SphereCollider m_sphereCollider;
    
    [SerializeField] protected float m_visionRadius = 50.0f;

    #endregion Variables

    protected override void Awake()
    {
        base.Awake();
        
        m_sphereCollider = GetComponent<SphereCollider>();
        m_sphereCollider.isTrigger = true;
        
        type = EType.VISION;
    }

    public override void Update()
    {
        m_sphereCollider.radius = m_visionRadius;
    }
}
