using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Terrain))]
public class CreateTrees : MonoBehaviour
{
    #region Variables

    private Terrain m_terrain;
    private TreeInstance[] originalTrees;

    #endregion Variables


    private void Awake()
    {
        m_terrain = GetComponent<Terrain>();
        originalTrees = m_terrain.terrainData.treeInstances;
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
        
        // Remove all painted trees
        terrainData.treeInstances = new TreeInstance[0];
        m_terrain.Flush();
    }

    // On scene end, repopulate the terrain data again with the trees
    private void OnDestroy()
    {
        m_terrain.terrainData.treeInstances = originalTrees;
        m_terrain.Flush();
    }
}
