using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Tables;

public class ObjectSpawnOnTime : MonoBehaviour
{
    public GameObject prefab;

    public List<float> spawnTime;
    public List<Transform> spawnPositions;
    private int totalCount;
    public Vector3 randomOffset;

    public ObjectPull<GameObject> objectPull;

    public void OnEnable()
    {
        totalCount = spawnTime.Count;

        if (totalCount == 0) { return; }

        objectPull = new ObjectPull<GameObject> (prefab, totalCount);


        coroutine = StartCoroutine(WaitAndSpawn(0));
    }
    public void OnDisable()
    {
        if (coroutine == null) { return; }
        StopCoroutine(coroutine);
    }


    public Coroutine coroutine;

    public IEnumerator WaitAndSpawn(int index)
    {
        yield return new WaitForSeconds(spawnTime[index]);
        GameObject obj = objectPull.GetObject();
        obj.transform.position = spawnPositions[Random.Range(0, spawnPositions.Count)].transform.position
            + new Vector3(Random.Range(-randomOffset.x, randomOffset.x), Random.Range(-randomOffset.y, randomOffset.y), Random.Range(-randomOffset.z, randomOffset.z));

        if (index + 1 < totalCount) 
        {
            coroutine = StartCoroutine(WaitAndSpawn(index + 1));
        }
    }
}
