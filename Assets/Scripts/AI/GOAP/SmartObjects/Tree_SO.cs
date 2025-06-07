using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioClip))]
public class Tree_SO : SmartObject
{
    #region Variables

    private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_audioClip;

    #endregion
    
    
    protected override void Awake()
    {
        m_interactionTime = 5.0f;

        m_audioSource = GetComponent<AudioSource>();
    }

    public override void Interact(GameObject _agent)
    {
        _agent.GetComponent<Lumberjack_AI>().isInteracting = true;
        m_audioSource.PlayOneShot(m_audioClip);
        StartCoroutine(Wait(m_interactionTime, _agent));
    }

    public IEnumerator Wait(float _duration, GameObject _agent)
    {
        
        yield return new WaitForSeconds(_duration);
        _agent.GetComponent<Lumberjack_AI>().isInteracting = false;
        Destroy(gameObject);
    }
}
