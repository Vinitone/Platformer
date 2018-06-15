using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    public Stat health;
    public GameObject bar, bullet, player;
    public Transform[] teleportPos = new Transform[3];
    private Vector3 targetScale, baseScale, shootDir;
    public float maxSize, minSize, scaleSpeed, bulletSpeed;
    private float currScale;
    public Color color;
    

    void Start()
    {
        health.Initialize();
        baseScale = transform.localScale * maxSize;
    }

    // Update is called once per frame
    void Update()
    {
        //health.Regen(1, 15);
        GetDirection();
        Death(this.gameObject, health.CurrentVal);
        ScaleDown();
        if (health.CurrentVal <= 0)
            bar.SetActive(false);
        if (health.CurrentVal > 750)
            States(1);
        else if (health.CurrentVal > 500)
            States(2);
        else if (health.CurrentVal > 250)
            States(3);
        else if (health.CurrentVal > 0)
            States(4);

    }

    private void States(int hp)
    {
        switch (hp)
        {
            case 1:
                if (Cooldown(1))
                {
                    Shoot(3);
                }
                GetComponent<Renderer>().material.color = Color.green;
                if (Cooldown(15))
                    Teleport(Random.Range(0, 3));
                break;
            case 2:
                GetComponent<Renderer>().material.color = Color.yellow;
                if (Cooldown(10))
                    Teleport(Random.Range(0, 3));
                break;
            case 3:
                GetComponent<Renderer>().material.color = color;
                if (Cooldown(7.5f))
                    Teleport(Random.Range(0, 3));
                break;
            case 4:
                GetComponent<Renderer>().material.color = Color.red;
                if (Cooldown(5))
                    Teleport(Random.Range(0,3));
                break;
        }
    }

    private void AttackPattern()
    {

    }

    private void Teleport(int position)
    {
        switch (position)
        {
            case 0:
                this.transform.position = teleportPos[0].position;
                break;
            case 1:
                this.transform.position = teleportPos[1].position;
                break;
            case 2:
                this.transform.position = teleportPos[2].position;
                break;
        }
    }

    private void ScaleDown()
    {
        currScale = health.CurrentVal / health.MaxVal;
        targetScale = baseScale * currScale;

        targetScale.x = Mathf.Clamp(targetScale.x, minSize, maxSize);
        targetScale.y = Mathf.Clamp(targetScale.y, minSize, maxSize);
        if (this.transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
        }
    }

    private void Shoot(int bullets)
    {
        if (bullets == 1)
        {
            var instance = Instantiate(bullet, this.transform.position, Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
        }
        else
        {
            float degrees = 360 / bullets;
            for (int i = 0; bullets > i; i++)
            {
                var instance = Instantiate(bullet, this.transform.position, Quaternion.identity);
                instance.GetComponent<Rigidbody2D>().velocity = Vector3.up * bulletSpeed;
            }
        }
    }

    private void GetDirection()
    {
        var playerPos = player.transform.position;

        shootDir = playerPos - transform.position;
        shootDir.z = 0.0f;
        shootDir = shootDir.normalized;
    }
}
