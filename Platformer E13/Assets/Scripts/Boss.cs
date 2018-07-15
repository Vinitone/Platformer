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
        anim = GetComponent<Animator>();
        StartCoroutine(Teleport(8));
        StartCoroutine(AttackPattern(6));
    }

    // Update is called once per frame
    void Update()
    {
        health.Regen(1, 5);
        if (health.CurrentVal <= 0)
            lvlChanger.SetTrigger("Fade Out");
        Death(this.gameObject, health.CurrentVal);
        ScaleDown();
        States();  
    }

    private void States()
    {
        if (health.CurrentVal > 750)
            child.GetComponent<SpriteRenderer>().material.color = Color.green;
        else if (health.CurrentVal > 500)
            child.GetComponent<SpriteRenderer>().material.color = Color.yellow;
        else if (health.CurrentVal > 250)
            child.GetComponent<SpriteRenderer>().material.color = color;
        else if (health.CurrentVal > 0)
            child.GetComponent<SpriteRenderer>().material.color = Color.red;
    }

    private IEnumerator AttackPattern( float delayTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            int animation = Random.Range(0, 5);
            switch (animation)
            {
                case 0:
                    anim.SetTrigger("JumpAttack");
                    break;
                case 1:
                    anim.SetTrigger("Bash");
                    break;
                case 2:
                    anim.SetTrigger("Cone");
                    break;
                case 3:
                    anim.SetTrigger("Laser");
                    break;
            }
        }
    }

    private IEnumerator Teleport(float delayTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            int position = Random.Range(0, 3);
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
    }
}
