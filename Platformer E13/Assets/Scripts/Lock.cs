using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lock : MonoBehaviour {

    public Key[] keys;
    public Sprite sprite;
    public bool endDoor;
    public Animator anim;
    void Update () {

        foreach (Key key in keys)
        {
            if (key.GetComponent<Key>().Open)
            {
                if (endDoor)
                    GetComponent<SpriteRenderer>().sprite = sprite;
                else
                    Destroy(this.gameObject);
            }
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (endDoor && keys.All(x => x.Open == true) && collision.tag == "Player")
            anim.SetTrigger("Fade Out");
        
    }
}
