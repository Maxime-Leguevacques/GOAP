using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tree_SO : SmartObject
{
    protected override void Awake()
    {
        m_interactionTime = 5.0f;
    }

    public override void Interact(GameObject _agent)
    {
        _agent.GetComponent<Lumberjack_AI>().isInteracting = true;
        StartCoroutine(Wait(m_interactionTime, _agent));
    }

    public IEnumerator Wait(float _duration, GameObject _agent)
    {
        yield return new WaitForSeconds(_duration);
        _agent.GetComponent<Lumberjack_AI>().isInteracting = false;
        Destroy(gameObject);
    }
}
