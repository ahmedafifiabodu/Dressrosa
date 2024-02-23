using System.Collections.Generic;
using UnityEngine;

public class DestoryPE : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToDestroy;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private PlayerIsometricMovement script;

    public void DestroyAndSpawn()
    {
        // Destroy the objects
        foreach (var obj in objectsToDestroy)
        {
            Destroy(obj);
        }

        // Spawn the new object
        GameObject newObj = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        script._invertDirection = false;
    }
}