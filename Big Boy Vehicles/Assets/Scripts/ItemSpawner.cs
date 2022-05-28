using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]  private float timeBetweenSpawns = .5f;
    public bool spawnEnabled;

    private ObjectPooler objectPooler;
    private float nextItemSpawnTime;

    private void Start()
    {
        // Need to generate a random seed?
        //System.DateTime.Now.Ticks;

        objectPooler = ObjectPooler.Instance;
        nextItemSpawnTime = Time.time + timeBetweenSpawns;
    }

    private void Update()
    {
        if (spawnEnabled && Time.time > nextItemSpawnTime)
        {
            nextItemSpawnTime = Time.time + timeBetweenSpawns;
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        objectPooler.SpawnFromPool("Rocks", transform.position, Quaternion.identity);
    }

}
