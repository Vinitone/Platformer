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
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PlayerBullet)
        {
            if (collision.gameObject.tag == "Player")
            {
                player.GetComponent<PlatformerCharacter2D>().health.Reduce(damage);
                CombatTextManager.Instance.CreateText(collision.gameObject.transform.position, new Vector3(0, 1, 0), 2, 0, 2, "-" + damage.ToString(), Color.red, CombatText.TextType.FeedbackText);
                Destroy(this.gameObject);
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
                if (instance.GetComponentInParent<Boss>() != null)
                    instance.GetComponentInParent<Boss>().health.Reduce(damage);
                Destroy(this.gameObject);
            }
            else if (collision != null && collision.gameObject.GetComponent<Attract>() != null)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().mass += 5;
                collision.transform.localScale *= 1.1f;
                Destroy(this.gameObject);
            }
            else if (collision != null && collision.gameObject.tag != "Player")
            {
                Destroy(this.gameObject);
            }
        }
    }
}
