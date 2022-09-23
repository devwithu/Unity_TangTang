using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D collider)
    {
        Debug.Log(collider.name);
        if(collider.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerController>().GetExp(1);
            Destroy(gameObject,0.1f);
        }
    }
}
