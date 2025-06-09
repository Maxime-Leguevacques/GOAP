using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Tree_SO : SmartObject
{
    #region Variables

    private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_audioClip;

    #endregion
    
    
    protected override void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.clip = m_audioClip;
    }

    public override void Interact(GameObject _agent)
    {
        _agent.GetComponent<Lumberjack_AI>().isInteracting = true;
        m_audioSource.Play();
        StartCoroutine(Wait(m_interactionTime, _agent));
    }

    public IEnumerator Wait(float _duration, GameObject _agent)
    {
        yield return new WaitForSeconds(_duration);
        _agent.GetComponent<Lumberjack_AI>().isInteracting = false;
        _agent.GetComponent<Lumberjack_AI>().targetGameObject = null;
        Destroy(gameObject);
    }
}
