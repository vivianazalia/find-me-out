using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField] Transform[] spawnPos;

    private int index;

    public Vector3 GetSpawnPosition()
    {
        Vector3 pos = spawnPos[index++].position;
        if (index >= spawnPos.Length)
        {
            index = 0;
        }
        return pos;
    }

    public void SetParent(GameObject obj, Transform parent)
    {
        obj.transform.SetParent(parent);
    }
}
