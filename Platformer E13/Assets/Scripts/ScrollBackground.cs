using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

    public float scrollSpeed;
    
	void Update () {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, 0);
        GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", offset);
	}
}
