using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Terrain))]
public class CreateTrees : MonoBehaviour
{
    #region Variables

    private Terrain m_terrain;

    #endregion Variables


    private void Awake()
    {
        m_terrain = GetComponent<Terrain>();
    }

    void Start()
    {
        TerrainData terrainData = m_terrain.terrainData;
        TreeInstance[] treeInstances = terrainData.treeInstances;
        TreePrototype[] treePrototypes = terrainData.treePrototypes;

        for (int i = 0; i < treeInstances.Length; i++)
        {
            TreeInstance treeInstance = treeInstances[i];
            Vector3 worldPos = Vector3.Scale(treeInstance.position, terrainData.size) + m_terrain.transform.position;
            GameObject treePrefab = treePrototypes[treeInstance.prototypeIndex].prefab;
            Instantiate(treePrefab, worldPos, Quaternion.identity);
        }
    }
}
