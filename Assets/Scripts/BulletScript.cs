using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private bool isShot = false;
    
    private Transform enemy;
    private EnemyScript enemyScript;

    public float bulletSpeed;
    public float bulletLife = 10f;

    private Vector3 direction;



    // Update is called once per frame
    void Update()
    {
        if(isShot)
        {
            Vector3 targetPos = transform.position + direction * bulletLife;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, bulletSpeed * Time.deltaTime);
            // if(transform.position == enemy.position)
            // {
            //     this.gameObject.SetActive(false);
            //     enemyScript.GetHit();
            // }
        }

    }
    
    public void ShootTheBullet(Transform enemy)
    {
        enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
        Vector3 enemyPos = enemy.position;
        direction = Vector3.Normalize(enemyPos - transform.position);
        isShot = true;
        this.enemy = enemy;
    }
}
