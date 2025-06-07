using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SmartObject : MonoBehaviour
{
    #region Variables

    [SerializeField] protected float m_interactionTime;

    #endregion Variables


    protected abstract void Awake();

    public abstract void Interact(GameObject _agent);
}
