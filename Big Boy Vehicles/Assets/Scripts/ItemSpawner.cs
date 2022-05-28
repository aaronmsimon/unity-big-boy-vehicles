using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] items;

    private float timeBetweenSpawns = .5f;
    private float nextItemSpawnTime;

    private void Start()
    {
        // Need to generate a random seed?
        //System.DateTime.Now.Ticks;

        nextItemSpawnTime = Time.time + timeBetweenSpawns;
    }

    private void Update()
    {
        if (GameManager.Instance.InputController.SpawnRockInput == 1 && Time.time > nextItemSpawnTime)
        {
            nextItemSpawnTime = Time.time + timeBetweenSpawns;
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        int itemIndex = Random.Range(0, items.Length - 1);
        Instantiate(items[itemIndex], transform.position, Quaternion.identity);
    }

}
