using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour {

    GameObject chain;
    public LayerMask InteractibleMask;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, InteractibleMask);
        if (hit.collider != null && hit.collider.gameObject.tag == "Interactible" && Input.GetKey(KeyCode.E))
        {
            chain = hit.collider.gameObject;
            if (chain.GetComponent<HingeJoint2D>() != null)
            {
                GetComponent<HingeJoint2D>().enabled = true;
                GetComponent<HingeJoint2D>().connectedBody = chain.GetComponent<Rigidbody2D>();
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            GetComponent<HingeJoint2D>().connectedBody = null;
            GetComponent<HingeJoint2D>().enabled = false;
        }
    }
}
