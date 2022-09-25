using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float spawnInterval = 1.0f;

    [SerializeField]
    private float enemySpeed = 1.0f;

    public GameObject parentGO;
    
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy ()
    {
        while (true)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(39.0f, -39.0f), Random.Range(23.0f, -23.0f));
            
            float dist = Vector3.Distance(spawnPosition,  GameManager.instance.playerController.gameObject.transform.position);
            
            if(dist < 10f)
                continue;
            
            
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity) as GameObject;
            enemy.transform.parent = parentGO.transform;
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(8.0f, -8.0f), Random.Range(8.0f, -8.0f));
            Destroy(enemy, 20);

            yield return new WaitForSeconds(Random.Range(spawnInterval - 0.5f, spawnInterval + 0.5f));
        }
    }
}
