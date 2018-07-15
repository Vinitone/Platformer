using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

    public bool displayOnce = false;
    private bool beenDisplayed = false;
	public void TriggerDialogue ()
	{
        if (!beenDisplayed)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            beenDisplayed = true;
        }

        if (displayOnce && beenDisplayed)
            Destroy(this.gameObject);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            TriggerDialogue();
    }
}
