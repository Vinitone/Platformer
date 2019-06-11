using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject {

    public float damage;
    private GameObject player;
    private Vector2 direction;
    public bool PlayerBullet;
    public AudioClip hit;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        //Destroy(this.gameObject, 4);
	}

    public void OnSpawn()
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        transform.GetComponent<Rigidbody2D>().velocity = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //SoundManager.instance.PlaySingle(hit);
        if (!PlayerBullet)
        {
            if (collision.gameObject.tag == "Player")
            {
                player.GetComponent<PlatformerCharacter2D>().health.Reduce(damage);
                CombatTextManager.Instance.CreateText(collision.gameObject.transform.position, new Vector3(0, 1, 0), 2, 0, 2, "-" + damage.ToString(), Color.red, CombatText.TextType.FeedbackText);
                BulletDeath();
            }
            else if (collision != null && collision.gameObject.tag != "Enemy")
            {
                BulletDeath();
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
                if (instance.GetComponentInParent<Boss>() != null)
                    instance.GetComponentInParent<Boss>().health.Reduce(damage);
                BulletDeath();
            }
            else if (collision != null && collision.gameObject.GetComponent<Attract>() != null)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().mass += 5;
                collision.transform.localScale *= 1.1f;
                BulletDeath();
            }
            else if (collision != null && collision.gameObject.tag != "Player")
            {
                BulletDeath();
            }
        }
    }

    public void BulletDeath()
    {
       // ObjectPool.Instance.AddToPool(gameObject);
    }
}
