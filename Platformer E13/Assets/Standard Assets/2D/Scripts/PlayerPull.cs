using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPull : MonoBehaviour {

    public float distance = 1f, throwFroce = 10f, timePressed;
    public float startTime;
    public LayerMask InteractibleMask;

    private PlatformerCharacter2D player;
    public float energyDrainInSec = 1, energyFloatDrain = 5;
    private float timeStamp;

    GameObject box, floatable;
    private FloatObject floatObject;
	// Use this for initialization
	void Start () {
        floatObject = FindObjectOfType<FloatObject>();
        player = GetComponent<PlatformerCharacter2D>();
    }
	
	// Update is called once per frame
	void Update () {

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, InteractibleMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.down * transform.localScale.y, distance, InteractibleMask);
        if (hit.collider != null && hit.collider.gameObject.tag == "Interactible" && Input.GetKey(KeyCode.E) && !floatObject.floating || hit2.collider != null && hit2.collider.gameObject.tag == "Interactible" && Input.GetKey(KeyCode.E) && !floatObject.floating)
        {
            if(hit.collider != null)
                box = hit.collider.gameObject;
            else if (hit2.collider != null)
                box = hit2.collider.gameObject;
            if (box.GetComponent<FixedJoint2D>() != null)
            {
                box.GetComponent<FixedJoint2D>().enabled = true;
                box.GetComponent<BoxPull>().pushed = true;
                box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            }
        }else if (Input.GetKeyUp(KeyCode.E))
        {
            if (box.GetComponent<FixedJoint2D>() != null)
            {
                box.GetComponent<FixedJoint2D>().enabled = false;
                box.GetComponent<BoxPull>().pushed = false;
                box.GetComponent<FloatObject>().posOffset = box.transform.position;
            }
            //box.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwFroce;

        }
        if (hit.collider != null && hit.collider.gameObject.tag == "Interactible" && Input.GetKeyDown(KeyCode.Q) && player.energy.CurrentVal > energyFloatDrain || hit2.collider != null && hit2.collider.gameObject.tag == "Interactible" && Input.GetKeyDown(KeyCode.Q) && player.energy.CurrentVal > energyFloatDrain)
        {
            if (hit.collider != null)
                floatable = hit.collider.gameObject;
            else if (hit2.collider != null)
                floatable = hit2.collider.gameObject;
            startTime = Time.fixedTime;
            floatable.GetComponent<FloatObject>().floatTime = 0;
        }
        if (hit.collider != null && hit.collider.gameObject.tag == "Interactible" && Input.GetKey(KeyCode.Q) && player.energy.CurrentVal > energyFloatDrain || hit2.collider != null && hit2.collider.gameObject.tag == "Interactible" && Input.GetKey(KeyCode.Q) && player.energy.CurrentVal > energyFloatDrain)
        {
            if (timeStamp <= Time.time)
            {
                player.energy.Reduce(energyFloatDrain);
                CombatTextManager.Instance.CreateText(transform.position, new Vector3(0, 1, 0), 2, 0, 2, "-" + energyFloatDrain.ToString(), Color.blue, CombatText.TextType.FeedbackText);
                timeStamp = Time.time + energyDrainInSec;
                floatable.GetComponent<FloatObject>().floatTime += 1;
            }
        }

        else if(Input.GetKeyUp(KeyCode.Q))
        {
            timePressed = Time.fixedTime - startTime;
            floatable.GetComponent<FloatObject>().timePressed = timePressed;
            floatable.GetComponent<FloatObject>().height = 1 * timePressed;
            floatable.GetComponent<FloatObject>().releaseTime = Time.fixedTime;
            floatable.GetComponent<FloatObject>().floating = true;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }
}
