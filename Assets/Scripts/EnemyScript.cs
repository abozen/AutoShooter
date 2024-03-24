using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private WeaponShoot weaponShoot;
    [SerializeField] private Transform player;
    [SerializeField] private float speed;

    public Rigidbody rb;
    [SerializeField] GameObject hitParticle;
    [SerializeField] Animator animator;


    public int health = 5;
    private bool isDead = false;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(isDead)
            return;
        
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        transform.LookAt(player);
    }

    public void GetHit(int damage = 1)
    {
        health--;
        GameObject hit = Instantiate(hitParticle, transform);
        hit.transform.parent = transform;
        if (health == 0)
        {
            Die();
        }
        else
            animator.SetTrigger("isHit");
    }

    private void Die()
    {
        weaponShoot.KillTheEnemy(this.gameObject);
        animator.SetTrigger("isDead");
        this.gameObject.tag = "Untagged";
        Debug.Log(this.gameObject.tag);
        isDead = true;
        rb.isKinematic = true;
        //Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            GetHit();
            other.gameObject.SetActive(false);
        }
    }
}
