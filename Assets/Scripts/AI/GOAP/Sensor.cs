using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sensor : MonoBehaviour
{
    #region Variables

    protected Dictionary<string, object> m_updatePlanPreconditions;

    protected Lumberjack_AI m_lumberjackAi; 

    public enum EType
    {
        VISION,
        HEARING
        // FEELING
    }

    public EType type;

    #endregion Variables


    protected virtual void Awake()
    {
        m_lumberjackAi = GetComponent<Lumberjack_AI>();
    }
    
    public abstract void Update();

}
