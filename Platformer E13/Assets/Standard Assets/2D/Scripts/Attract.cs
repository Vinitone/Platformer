using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour {

    const float G = 500f;
    public Rigidbody2D rb;

    void Attracting()
    {
        Vector3 direction = transform.position - (Vector3)rb.position;
        float distance = direction.magnitude;

        if (distance == 0f)
            return;

        float forceMagnitude = G * (GetComponent<Rigidbody2D>().mass * rb.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rb.AddForce(force);
    }
	// Update is called once per frame
	void FixedUpdate () {
        Attracting();
	}
}
