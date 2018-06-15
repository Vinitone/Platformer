using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy {

    public float damage = 20;
    public Stat health;
    public float bulletSpeed;
    public GameObject bullet, player;
    private Vector3 shootDir;
    float cooldown = 0;
    // Use this for initialization
    void Start () {
        health.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
        GetDirection();
        Death(this.gameObject, health.CurrentVal);
    }

    private void Shoot()
    {
        var instance = Instantiate(bullet, this.transform.position , Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
    }

    private void GetDirection()
    {
        var playerPos = player.transform.position;

        shootDir = playerPos - transform.position;
        shootDir.z = 0.0f;
        shootDir = shootDir.normalized;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       if(collision.tag == "Player")
        {
            if (Cooldown(2))
            {
                Shoot();
            }
        }
    }
}
