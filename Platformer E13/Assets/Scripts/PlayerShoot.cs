﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerShoot : MonoBehaviour {

    PlatformerCharacter2D player;
    public GameObject gun, bullet;
    public float bulletSpeed;
    private Vector3 mouseDir;
    private Quaternion rotation;
    float cooldown = 0;
    private Animator anim;
    // Use this for initialization
    void Start () {
        player = GetComponent<PlatformerCharacter2D>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetMouseButtonDown(0) && player.ammo.CurrentVal > 0)
        {
            if(Cooldown(.3f))
                Shoot();
        }
        anim.SetBool("Reload", Input.GetKeyDown(KeyCode.R));
    }

    private void LateUpdate()
    {
        GetDirection();
    }
    private void Shoot()
    {
        var instance = Instantiate(bullet, gun.transform.position, rotation);
        instance.GetComponent<Rigidbody2D>().velocity = mouseDir * bulletSpeed;
        if(player.ammo.CurrentVal > 0)
            player.ammo.Reduce(1);
    }

    private void Reload()
    {
        player.ammo.Add(6);
    }

    private bool Cooldown(float seconds)
    {
        if (Time.time >= cooldown)
        {
            cooldown = Time.time + seconds;
            return true;
        }
        return false;
    }

    private void GetDirection()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        mouseDir = mousePos - transform.position;
        mouseDir.z = 0.0f;
        mouseDir = mouseDir.normalized;
        rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg);
        
        gun.transform.rotation = rotation;

        if (!player.m_FacingRight)
        {
            gun.transform.Rotate(Vector3.forward * 180);
        }
    }
}