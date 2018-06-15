using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour {
    [SerializeField]
    private Transform door;
    private bool locked = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !locked)
        {
            door.position += new Vector3(0, 14.7f, 0);

            locked = true;
        }
    }
}
