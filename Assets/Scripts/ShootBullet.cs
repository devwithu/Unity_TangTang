using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float shootInterval = 1.0f;
    
    [SerializeField]
    private float overlapSphereRadius = 13f;
    public void ReapeatSpawnBullet ()
    {
        InvokeRepeating("Shoot", this.shootInterval, this.shootInterval);
    }

    void Shoot()
    {
        float nearestDistance = overlapSphereRadius;
        GameObject nearestGO = null;
        
        var colliders = Physics.OverlapSphere(this.transform.position, overlapSphereRadius);
        //Debug.Log(colliders.Length);
        foreach(Collider hit in colliders)
        {
            //Debug.Log(hit.gameObject.tag);
            
            if (!hit.gameObject.CompareTag("Enemy"))
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
            //Debug.Log(nearestGO.name);
            var heading = nearestGO.transform.position - gameObject.transform.position;
            var direction = heading / nearestDistance; // This is now the normalized direction.

            CmdShoot(direction);
            //SoundManager.instance.PlaySFX("Shoot");
        }

    }

    void CmdShoot (Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().shooter = gameObject;

        Vector2 dirVector2 = new Vector2(direction.x, direction.y);
        bullet.GetComponent<Rigidbody2D>().velocity = dirVector2 * bulletSpeed;
        //NetworkServer.Spawn(bullet);
        Destroy(bullet, 1.0f);
    }
    
    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
    //     Gizmos.DrawWireSphere (transform.position, overlapSphereRadius);
    // }
}
