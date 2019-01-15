using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour {

    public Animator animator;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            animator.SetBool("IsOpen", true);
        }
        else
        {
            animator.SetBool("IsOpen", false);
        }
    }
}
