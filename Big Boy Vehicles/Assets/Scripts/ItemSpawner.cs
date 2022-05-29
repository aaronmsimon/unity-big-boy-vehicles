using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float timeBetweenSpawns = .5f;
    [SerializeField] public string itemPool;

    private ObjectPooler objectPooler;
    private float nextItemSpawnTime;
    [HideInInspector] public bool spawnEnabled;

    private void Start()
    {
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
        objectPooler.SpawnFromPool(itemPool, transform.position, Quaternion.identity);
    }

}
