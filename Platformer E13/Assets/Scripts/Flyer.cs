using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : Enemy
{
    private Vector3 startPos;
    public Stat health;

    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        health.Initialize();
    }


    // Update is called once per frame
    void Update()
    {
        if (FlyDistance(5))
        {
            Flip(this.transform);
        }
        Move(this.transform, 3);
        health.Regen(1, 10);
        Death(this.gameObject, health.CurrentVal);
    }

    private bool FlyDistance(float distance)
    {
        if (startPos.x + distance <= transform.position.x)
        {
            transform.position = new Vector3(startPos.x + distance, startPos.y);
            return true;
        }
        if(startPos.x - distance >= transform.position.x)
        {
            transform.position = new Vector3(startPos.x - distance, startPos.y);
            return true;
        }
        return false;
    }
}
