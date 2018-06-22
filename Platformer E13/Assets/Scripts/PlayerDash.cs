using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private PlatformerCharacter2D player;
    public float dashPower, energyDrainInSec = 1, energyDrainAmount;
    private float timeStamp;
    Vector2 tempY;
    public Vector3 mousePos, mouseDir;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        player = GetComponent<PlatformerCharacter2D>();
    }

    void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        mouseDir = mousePos - transform.position ;
        mouseDir.z = 0.0f;
        mouseDir = mouseDir.normalized;

        
        if (Input.GetKey(KeyCode.LeftShift) && player.energy.CurrentVal > energyDrainAmount )
        {
            if (timeStamp <= Time.time)
            {
                player.energy.Reduce(energyDrainAmount);
                CombatTextManager.Instance.CreateText(transform.position, new Vector3(0, 1, 0), 2, 0, 2, "-" + energyDrainAmount.ToString(), Color.blue, CombatText.TextType.FeedbackText);
                timeStamp = Time.time + energyDrainInSec;
            }
            tempY = rigidbody2d.velocity;
            tempY.y = 0;
            rigidbody2d.velocity = tempY;
            rigidbody2d.velocity = mouseDir * dashPower;
        }
    }
}