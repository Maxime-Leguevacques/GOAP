using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SharedMemory
{
    #region Variables

    public static List<GameObject> objectsInInteraction = new();
    
 
    #endregion

    public static bool IsGameObjectAvailable(GameObject _gameObject)
    {
        if (objectsInInteraction.Contains(_gameObject))
        {
            return false;
        }

        return true;
    }
}
