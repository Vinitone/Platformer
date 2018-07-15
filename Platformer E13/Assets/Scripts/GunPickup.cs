using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour {

    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.GetComponent<PlayerShoot>().enabled = true;
            player.GetComponent<PlayerShoot>().gun.SetActive(true);
            Destroy(gameObject);
        }
    }
}
