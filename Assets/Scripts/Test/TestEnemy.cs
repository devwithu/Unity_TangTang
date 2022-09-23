using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public string enemyTag;
    void OnTriggerEnter2D (Collider2D collider)
    {
        //Debug.Log(collider.name);
        if(collider.tag == this.enemyTag)
        {
            Destroy(collider.gameObject);
        }
    }

    private void Start()
    {
        ReapeatSpawnBullet();
    }
    
    public void ReapeatSpawnBullet ()
    {
        InvokeRepeating("Shoot", 1, 1);
    }

    void Shoot()
    {
        var colliders = Physics.OverlapSphere(this.transform.position, 10);
        Debug.Log(colliders.Length);
        foreach(Collider hit in colliders)
        {
            Debug.Log(hit.name);
            if(hit.tag == "Player")
            {

            }
        } 

    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere (transform.position, 10);
    }
}
