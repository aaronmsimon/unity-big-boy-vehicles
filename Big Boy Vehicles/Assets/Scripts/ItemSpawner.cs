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
        if (ignoreIfOverlap)
        {
            objectPooler.SpawnFromPool(itemPool, transform.position, Quaternion.identity);
        } else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, overlapRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.name != "Ground")
                {
                    return;
                }
            }
            objectPooler.SpawnFromPool(itemPool, transform.position, Quaternion.identity);
        }
    }

}
