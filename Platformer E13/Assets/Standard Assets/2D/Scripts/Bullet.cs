using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float damage;
    private GameObject player;
    public bool PlayerBullet;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PlayerBullet)
        {
            if (collision.gameObject.tag == "Player")
            {
                player.GetComponent<PlatformerCharacter2D>().health.Reduce(damage);
                Destroy(this.gameObject);
            }
            if(collision.gameObject.tag == "Enemy")
            {
                //Physics2D.IgnoreCollision();
               
            }
            else if (collision != null && collision.gameObject.tag != "Enemy")
            {
                Destroy(this.gameObject);
            }
        }
        if (PlayerBullet)
        {
            if (collision.gameObject.tag == "Enemy")
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
                Destroy(this.gameObject);
            }
            else if (collision != null && collision.gameObject.tag != "Player")
            {
                Destroy(this.gameObject);
            }
        }
    }
}
