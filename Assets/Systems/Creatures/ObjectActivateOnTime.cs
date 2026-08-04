using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivateOnTime : MonoBehaviour
{
    public List<float> spawnTime;
    public List<GameObject> objects;

    public void OnEnable()
    {
        if (spawnTime.Count == 0 || objects.Count == 0) { return; }
        if (spawnTime.Count != objects.Count ) { Debug.LogError("ObjectActiveOnTime: spawnTime.Count != objects.Count"); return; }

        coroutine = StartCoroutine(WaitAndSpawn(0));

        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }
    public void OnDisable()
    {
        if (coroutine == null) { return; }

        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
        StopCoroutine(coroutine);
    }


    public Coroutine coroutine;

    public IEnumerator WaitAndSpawn(int index)
    {
        yield return new WaitForSeconds(spawnTime[index]);
        GameObject obj = objects[index];
        obj.SetActive(true);

        if (index + 1 < spawnTime.Count)
        {
            coroutine = StartCoroutine(WaitAndSpawn(index + 1));
        }
    }
}
