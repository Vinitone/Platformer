using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : Enemy {
    public Stat health;
	void Start () {
        health.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
        if(!GroundDetection(this.transform.position, 1f))
        {
            Flip(this.transform);
        }
        Move(this.transform, 3);
        health.Regen(1, 10);
        Death(this.gameObject, health.CurrentVal);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            Debug.Log("collision");
            Flip(this.transform);
        }
    }
}
