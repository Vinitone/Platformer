using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerSlash : MonoBehaviour {

    PlatformerCharacter2D player;
    public GameObject fullSlash, quarterSlash, blackFullSlash, blackQuarterSlash, bullet;
    public bool slash = false;
    public float damage, bulletSpeed;
    ParticleSystem.MainModule main, main2, main3, main4;
    private Vector3 mouseDir;
    float cooldown = 0;
    // Use this for initialization
    void Start () {
        player = GetComponent<PlatformerCharacter2D>();

        //main = fullSlash.GetComponent<ParticleSystem>().main;
        //main2 = quarterSlash.GetComponent<ParticleSystem>().main;
        //main3 = blackFullSlash.GetComponent<ParticleSystem>().main;
        //main4 = blackQuarterSlash.GetComponent<ParticleSystem>().main;
    }
	
	// Update is called once per frame
	void Update () {
        slash = false;
        GetDirection();
        if (Input.GetMouseButtonDown(1))
        {
            if(Cooldown(1f))
                Shoot();
        }
        if (Input.GetMouseButtonDown(0) && player.m_Grounded)
        {
            quarterSlash.GetComponent<ParticleSystem>().Emit(1);
            blackQuarterSlash.GetComponent<ParticleSystem>().Emit(1);
            slash = true;
        }
        else if(Input.GetMouseButtonDown(0) && !player.m_Grounded)
        {
            fullSlash.GetComponent<ParticleSystem>().Emit(1);
            blackFullSlash.GetComponent<ParticleSystem>().Emit(1);
            slash = true;
        }
        if(!player.m_FacingRight)
        {
            main.startRotationY = 180 * Mathf.Deg2Rad;
            main2.startRotationY = 180 * Mathf.Deg2Rad;
            main3.startRotationY = 180 * Mathf.Deg2Rad;
            main4.startRotationY = 180 * Mathf.Deg2Rad;
        }
        if (player.m_FacingRight)
        {
            main.startRotationY = 0;
            main2.startRotationY = 0;
            main3.startRotationY = 0;
            main4.startRotationY = 0;
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && slash)
        {
            var instance = collision.gameObject;
            if (instance.GetComponent<Flyer>() != null)
                instance.GetComponent<Flyer>().health.Reduce(damage);
            if (instance.GetComponent<Crawler>() != null)
                instance.GetComponent<Crawler>().health.Reduce(damage);
            if (instance.GetComponent<Shooter>() != null)
                instance.GetComponent<Shooter>().health.Reduce(damage);
            if (instance.GetComponent<Boss>() != null)
                instance.GetComponent<Boss>().health.Reduce(damage);
        }
    }

    private void Shoot()
    {
        var instance = Instantiate(bullet, this.transform.position, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().velocity = mouseDir * bulletSpeed;
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
    }
}
