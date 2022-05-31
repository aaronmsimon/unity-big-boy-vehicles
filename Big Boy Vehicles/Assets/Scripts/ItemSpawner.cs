using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float timeBetweenSpawns = .5f;
    [SerializeField] private float spawnRange;
    [SerializeField] public string itemPool;
    [SerializeField] private bool ignoreIfOverlap;
    [SerializeField] private float overlapRadius = 1f;

    private ObjectPooler objectPooler;
    private float nextItemSpawnTime;
    public bool spawnEnabled;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        nextItemSpawnTime = Time.time + timeBetweenSpawns + Random.Range(-spawnRange, spawnRange);
    }

    private void Update()
    {
        if (spawnEnabled && Time.time > nextItemSpawnTime)
        {
            nextItemSpawnTime = Time.time + timeBetweenSpawns + Random.Range(-spawnRange, spawnRange);
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        bool canSpawn = true;

        if (!ignoreIfOverlap)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, overlapRadius);
            foreach (Collider collider in colliders)
            {
                if (itemPool == (collider.transform.name.Length > 2 ? collider.transform.name.Substring(0,collider.transform.name.Length - 2) : collider.transform.name))
                {
                    canSpawn = false;
                    break;
                }
            }
        }

        if (ignoreIfOverlap || canSpawn)
            objectPooler.SpawnFromPool(itemPool, transform.position, Quaternion.identity);
    }

}
