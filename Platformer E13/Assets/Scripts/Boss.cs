using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    public Stat health;
    public GameObject bullet, player, gun;
    public Transform[] teleportPos = new Transform[3];
    private Vector3 targetScale, baseScale, shootDir;
    public float maxSize, minSize, scaleSpeed, bulletSpeed;
    private float currScale;
    public Color color;
    private Animator anim;
    public Animator lvlChanger;
    private Transform child;

    void Start()
    {
        child = transform.GetChild(0);
        health.Initialize();
        baseScale = child.localScale;
        anim = GetComponentInParent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        health.Regen(1, 5);
        GetDirection();
        if (health.CurrentVal <= 0)
            lvlChanger.SetTrigger("Fade Out");
        Death(this.gameObject, health.CurrentVal);
        ScaleDown();

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
                    
                }
                child.GetComponent<SpriteRenderer>().material.color = Color.green;
                if (Cooldown(15))
                    Teleport(Random.Range(0, 3));
                break;
            case 2:
                child.GetComponent<SpriteRenderer>().material.color = Color.yellow;
                if (Cooldown(10))
                    Teleport(Random.Range(0, 3));
                break;
            case 3:
                if (!anim.GetBool("GunInit"))
                {
                    anim.SetBool("GunInit", true);
                    anim.SetTrigger("Gun");
                }
                child.GetComponent<SpriteRenderer>().material.color = color;
                if (Cooldown(7.5f))
                    Teleport(Random.Range(0, 3));
                break;
            case 4:
                child.GetComponent<SpriteRenderer>().material.color = Color.red;
                if (Cooldown(5))
                    Teleport(Random.Range(0, 3));
                break;
        }
    }

    private void AttackPattern(int animation)
    {
        switch (animation)
        {
            case 0:
                anim.SetTrigger("JumpAttack");
                break;
            case 1:
                anim.SetTrigger("Bash");
                break;
            case 2:
                break;
            case 3:
                break;
        }
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
        if (child.localScale != targetScale)
        {
            child.localScale = Vector3.Lerp(child.localScale, targetScale, scaleSpeed * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        var instance = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
        instance.GetComponent<Rigidbody2D>().velocity = Vector3.left * bulletSpeed;
        Debug.Log("Boss Shot!");
    }

    private void GetDirection()
    {
        var playerPos = player.transform.position;

        shootDir = playerPos - child.position;
        shootDir.z = 0.0f;
        shootDir = shootDir.normalized;
    }

    private void GunInit()
    {
        Debug.Log("PEW");
        gun.SetActive(false);
    }
}
