using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    List<GameObject> closeEnemies = new List<GameObject>();
    private GameObject currentBullet;
    [SerializeField] private ObjectPool bulletPool;

    [SerializeField] private float shootingRate;
    private float previousTime;

    [SerializeField] private ParticleSystem shootParticle;
    [SerializeField] Transform gunPoint;

    // Start is called before the first frame update
    void Start()
    {
        previousTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    private void Shoot()
    {
        if (Time.time - previousTime < shootingRate || closeEnemies.Count == 0)
        {
            return;
        }
        previousTime = Time.time;

        GameObject currentEnemy = GetClosestEnemy();
        transform.LookAt(currentEnemy.transform);
        shootParticle.Play();
        currentBullet = bulletPool.GetPooledObject(0);
        currentBullet.transform.position = gunPoint.position;
        currentBullet.SetActive(true);
        currentBullet.GetComponent<BulletScript>().ShootTheBullet(currentEnemy.transform);

    }

    GameObject GetClosestEnemy()
    {
        GameObject closestEnemy = closeEnemies[0];
        float minDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);
        foreach (GameObject enemy in closeEnemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);
            if (enemyDistance < minDistance)
            {
                closestEnemy = enemy;
                minDistance = enemyDistance;
            }
        }

        return closestEnemy;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy tirgger enter");
            closeEnemies.Add(other.gameObject);
            other.gameObject.name = "closeEnemy";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.name = "Enemy";
            closeEnemies.Remove(other.gameObject);
        }
    }

    public void KillTheEnemy(GameObject enemy)
    {
        Debug.Log("kill the : " + enemy.name);
        closeEnemies.Remove(enemy);
    }
}
