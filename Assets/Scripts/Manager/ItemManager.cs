using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPrefab;

    [SerializeField]
    private float spawnInterval = 1.0f;

    public GameObject parentGO;

    private void Start()
    {
        StartCoroutine(SpawnItem());
    }

    IEnumerator SpawnItem ()
    {
        while (true)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(39.0f, -39.0f), Random.Range(23.0f, -23.0f));
            GameObject item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity) as GameObject;
            item.transform.parent = parentGO.transform;
            Destroy(item, 10);

            yield return new WaitForSeconds(Random.Range(spawnInterval - 0.5f, spawnInterval + 0.5f));
        }
    }
}
