using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

    public Key key;
    public Sprite sprite;
    public bool endDoor;

	void Update () {
        if (key.GetComponent<Key>().Open)
        {
            if (endDoor)
                GetComponent<SpriteRenderer>().sprite = sprite;
            else
                Destroy(this.gameObject);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (endDoor && key.GetComponent<Key>().Open && collision.tag == "Player")
             ManagerForScenes.Instance.NextLevelScreen();
        
    }
}
