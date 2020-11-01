using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Prefab;

    public bool SpawnOnEnable = true;


    public float spawnFrequency = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if(spawnFrequency > 0f)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    IEnumerator SpawnRoutine()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(1f / spawnFrequency);
            Spawn();
        }
    }

    private void OnEnable()
    {
        if (SpawnOnEnable)
            Spawn();
    }

    public void Spawn()
    {
        Instantiate(Prefab, transform.position, transform.rotation);
    }
}
