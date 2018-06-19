using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatObject : MonoBehaviour {

    private GameObject floatObject, text;
    public bool floating;
    public float amplitude, frequency, height, timePressed, floatTime = 0, speed, releaseTime;

    private float timeStamp;
    public LayerMask floatMask;
    public Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    // Use this for initialization
    void Start () {
        posOffset = transform.position;
        text = CombatTextManager.Instance.CreateText(transform.position, new Vector3(0, 0, 0), 0, 0, 0, floatTime.ToString(), Color.grey, CombatText.TextType.TimerText);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        text.transform.position = this.transform.position;
        text.GetComponent<Text>().text = floatTime.ToString();
        
        if (floating)
        {
            tempPos = posOffset;
            tempPos.y += height;
            float distCovered = (Time.fixedTime - releaseTime) * speed;
            float journey = distCovered / (tempPos.y - posOffset.y);
            transform.position = Vector3.Lerp(this.transform.position, tempPos, journey);
            if (timeStamp <= Time.time)
            {

                timeStamp = Time.time + 1;
                floatTime -= 1;
            }
        }
        if (transform.position == tempPos && floating) 
        {
            tempPos = transform.position;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
            
        }
        if(Time.fixedTime > releaseTime + (int)timePressed && floating || floatTime < 0)
        {
            floatTime = 0;
            floating = false;

        }
        if(!floating)
        {
            tempPos = transform.position;
            tempPos.y -= height;
            float distCovered = (Time.fixedTime - releaseTime) * speed;
            float journey = distCovered / (tempPos.y - posOffset.y);
            transform.position = Vector3.Lerp(this.transform.position, tempPos, journey);
        }

        

    }
}
