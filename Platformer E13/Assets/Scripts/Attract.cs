using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour {

    const float G = 500f;
    public float criticalMass;
    public Rigidbody2D player;

    void Attracting()
    {
        Vector3 direction = transform.position - (Vector3)player.position;
        float distance = direction.magnitude;

        if (distance == 0f)
            return;

        float forceMagnitude = G * (GetComponent<Rigidbody2D>().mass * player.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        player.AddForce(force);
    }
	// Update is called once per frame
    void ReachCriticalMass()
    {
        if(GetComponent<Rigidbody2D>().mass > criticalMass)
        {
            Destroy(this.gameObject);
        }
    }
	void FixedUpdate () {
        
        ReachCriticalMass();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(player.gameObject.tag == collision.tag)
            Attracting();
    }
}
