using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerShoot : MonoBehaviour {

    PlatformerCharacter2D player;
    public GameObject gun, bullet;
    public float bulletSpeed;
    private Vector3 mouseDir;
    float cooldown = 0;
    // Use this for initialization
    void Start () {
        player = GetComponent<PlatformerCharacter2D>();
    }
	
	// Update is called once per frame
	void Update () {
        GetDirection();
        if (Input.GetMouseButtonDown(1))
        {
            if(Cooldown(1f))
                Shoot();
        }
	} 
    private void Shoot()
    {
        var instance = Instantiate(bullet, gun.transform.position, Quaternion.identity);
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
