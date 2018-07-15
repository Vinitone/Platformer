using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPull : MonoBehaviour {

    public float distance = 1f;
    public LayerMask InteractibleMask;

    GameObject box;
    FloatObject floatObject;
	// Use this for initialization
	void Start () {
        floatObject = FindObjectOfType<FloatObject>();
    }
	
	// Update is called once per frame
	void Update () {

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D[] hits = new RaycastHit2D[2];
        hits[0] = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, InteractibleMask);
        hits[1] = Physics2D.Raycast(transform.position, Vector2.down * transform.localScale.y, distance, InteractibleMask);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject.tag == "Interactible" && Input.GetKey(KeyCode.E) && !floatObject.floating)
            {
                box = hit.collider.gameObject;
                if (box.GetComponent<FixedJoint2D>() != null)
                {
                    box.GetComponent<FixedJoint2D>().enabled = true;
                    box.GetComponent<BoxPull>().pushed = true;
                    box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
                }
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                if (box.GetComponent<FixedJoint2D>() != null)
                {
                    box.GetComponent<FixedJoint2D>().enabled = false;
                    box.GetComponent<BoxPull>().pushed = false;
                }
            }
        }
    }
}
