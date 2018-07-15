using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloat : MonoBehaviour {
    GameObject floatable;
    public float energyDrainInSec = 1, energyFloatDrain = 5, distance = 1f;
    [HideInInspector]
    public float timePressed, startTime;
    PlatformerCharacter2D player;
    [SerializeField]
    private LayerMask InteractibleMask;
    private float timeStamp;
    // Use this for initialization
    void Start () {
        player = GetComponent<PlatformerCharacter2D>();
    }
	
	// Update is called once per frame
	void Update () {

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D[] hits = new RaycastHit2D[2];
        hits[0] = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, InteractibleMask);
        hits[1] = Physics2D.Raycast(transform.position, Vector2.down * transform.localScale.y, distance, InteractibleMask);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject.tag == "Interactible" && Input.GetKeyDown(KeyCode.Q) && player.energy.CurrentVal > energyFloatDrain)
            {
                floatable = hit.collider.gameObject;
                startTime = Time.fixedTime;
                floatable.GetComponent<FloatObject>().floatTime = 0;
                floatable.GetComponent<FloatObject>().posOffset = floatable.transform.position;
            }
            if (hit.collider != null && hit.collider.gameObject.tag == "Interactible" && Input.GetKey(KeyCode.Q) && player.energy.CurrentVal > energyFloatDrain)
            {
                if (timeStamp <= Time.time)
                {
                    player.energy.Reduce(energyFloatDrain);
                    CombatTextManager.Instance.CreateText(transform.position, new Vector3(0, 1, 0), 2, 0, 2, "-" + energyFloatDrain.ToString(), Color.blue, CombatText.TextType.FeedbackText);
                    timeStamp = Time.time + energyDrainInSec;
                    floatable.GetComponent<FloatObject>().floatTime += 1;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                timePressed = Time.fixedTime - startTime;
                floatable.GetComponent<FloatObject>().timePressed = timePressed;
                floatable.GetComponent<FloatObject>().releaseTime = Time.fixedTime;
                floatable.GetComponent<FloatObject>().floating = true;
            }
        }
    }
}
