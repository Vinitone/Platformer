using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPull : MonoBehaviour {

    public bool pushed;
    float xPos;
	// Use this for initialization
	void Start () {
        xPos = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(!pushed)
        {
            transform.position = new Vector3(xPos, transform.position.y);
        }
        else
        {
            xPos = transform.position.x;
        }
	}
}
