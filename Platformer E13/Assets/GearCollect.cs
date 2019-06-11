using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearCollect : MonoBehaviour {

    int gearsCollected = 0;
    public int collectInt = 4;
    private string label;
    public Text text;
    public void CollectedGear()
    {
        gearsCollected += 1;
        label = "" + gearsCollected + " / " + collectInt;
        text.text = label;
    }
}
