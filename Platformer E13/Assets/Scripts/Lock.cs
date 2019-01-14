using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lock : MonoBehaviour {

    public Key[] keys;
    public Sprite sprite;
    public bool endDoor;
    public Animator anim;
    public AnimationClip clip;
    public int levelToLoad;
    AnimationEvent evt = new AnimationEvent();

    public void Start()
    {
        evt.time = 1;
        evt.functionName = "LoadLevel";   
    }
    void Update () {

        foreach (Key key in keys)
        {
            if (key != null && key.GetComponent<Key>().Open)
            {
                if (endDoor)
                    GetComponent<SpriteRenderer>().sprite = sprite;
                else
                    Destroy(this.gameObject);
            }
        }      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (endDoor && (keys.All(x => x.Open == true) || keys.Length == 0) && collision.tag == "Player" && Input.GetKeyDown(KeyCode.W))
        {
            evt.intParameter = levelToLoad;
            clip.AddEvent(evt);
            anim.SetTrigger("Fade Out");
        }
    }
}
