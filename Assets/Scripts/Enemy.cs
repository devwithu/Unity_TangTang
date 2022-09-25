using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival
{
    
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 10;

    private int currentHealth;

    [SerializeField]
    private string enemyTag;
    [SerializeField]
    private float overlapSphereRadius = 50f;
    
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    
    void Start ()
    {
        this.currentHealth = this.maxHealth;
        rb = this.GetComponent<Rigidbody2D>();

        StartCoroutine(FindTargetLoop());
    }

    // Update is called once per frame
    void Update(){

        if (null != player)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
        }
        else
        {

        }

    }

    IEnumerator FindTargetLoop()
    {
        while (true)
        {
            FindTarget();
            if (null == player)
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                break;
            }
        }
        StopCoroutine(FindTargetLoop());
    }
    
    void FindTarget()
    {
        float nearestDistance = overlapSphereRadius;
        GameObject nearestGO = null;
        
        var colliders = Physics.OverlapSphere(this.transform.position, overlapSphereRadius);
        //Debug.Log(colliders.Length);
        foreach(Collider hit in colliders)
        {
            //Debug.Log(hit.gameObject.tag);
            
            if (!hit.gameObject.CompareTag("Player"))
                continue;
            float Distance = Vector3.Distance(gameObject.transform.position, hit.transform.position);
 
            //Debug.Log(Distance);
            
            if (Distance < nearestDistance) // 위에서 잡은 기준으로 거리 재기
            {
                nearestGO = hit.gameObject;
                nearestDistance = Distance;
            }
        }

        if (nearestGO)
        {
            player = nearestGO.transform;
        }
    }
    private void FixedUpdate() {
        moveCharacter(movement);
    }
    void moveCharacter(Vector2 direction){
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        //Debug.Log(collider.name);
        if(collider.CompareTag(this.enemyTag) && collider.name != "Bullet1(Clone)")
        {
            if (tag.Equals("Boss"))
            {
                print(collider.name);
            }
            this.TakeDamage(1);
            Destroy(collider.gameObject,0.1f);
        }
    }

    void TakeDamage (int amount)
    {

            this.currentHealth -= amount;

            if(this.currentHealth <= 0)
            {
                //player.GetComponent<PlayerController>().currentExp += 1;
                //player.GetComponent<PlayerController>().PlaySoundGetExp();
                //RpcPlaySoundDamaged(player);
                Destroy(this.gameObject, 0.1f);
            }
      
    }

    public void RpcPlaySoundDamaged(GameObject player)
    {

            SoundManager.instance.PlaySFX("PlayerDamaged");

    }
    
    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
    //     Gizmos.DrawWireSphere (transform.position, overlapSphereRadius);
    // }
}
}