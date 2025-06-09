using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class PopulateRessources : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject[] branches;
    [SerializeField] private int branchCount = 100;
    [SerializeField] private GameObject[] ores;
    [SerializeField] private int oreCount = 35;
    [SerializeField] private float minRadius = 30;
    [SerializeField] private float maxRadius = 100;

    [Header("Debug")] 
    [SerializeField] private bool m_showRadius = false;

    #endregion Variables


    private void Awake()
    {
        PopulateBranches(branchCount);
        PopulateOres(oreCount);
    }

    private void PopulateBranches(int _branchCount)
    {
        for (int i = 0; i < _branchCount; i++)
        {
            Vector3 randomPos = GetRandomPosOnNavMesh();
            if (randomPos != Vector3.zero)
            {
                float randomRot = Random.Range(0, 360);
                GameObject branchPrefab = branches[Random.Range(0, branches.Length)];
                Instantiate(branchPrefab, randomPos, Quaternion.Euler(0, randomRot, 0));
            }
        }
    }
    
    private void PopulateOres(int _oreCount)
    {
        for (int i = 0; i < _oreCount; i++)
        {
            Vector3 randomPos = GetRandomPosOnNavMesh();
            if (randomPos != Vector3.zero)
            {
                float randomRot = Random.Range(0, 360);
                GameObject orePrefab = ores[Random.Range(0, ores.Length)];
                Instantiate(orePrefab, randomPos, Quaternion.Euler(0, randomRot, 0));
            }
        }
    }

    private Vector3 GetRandomPosOnNavMesh()
    {
        for (int i = 0; i < 10; i++)
        {
            // Get a random direction and distance between min and max radius
            Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius);
            Vector3 candidate = new Vector3(randomCircle.x, 0, randomCircle.y) + (transform.position + new Vector3(500, 0, 500));

            if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return Vector3.zero;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (m_showRadius)
        {
            Gizmos.DrawWireSphere((transform.position + new Vector3(500, 0, 500)), minRadius);
            Gizmos.DrawWireSphere((transform.position + new Vector3(500, 0, 500)), maxRadius);
        }
    }
}
