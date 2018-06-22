using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public Sprite sprite;
    public bool item;

    void Start()
    {
        Open = false;
    }
    public bool Open
    {
        get; set;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactible")
        {
            Open = true;
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
        else if (collision.tag == "Player" && item)
        {
            Open = true;
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}
