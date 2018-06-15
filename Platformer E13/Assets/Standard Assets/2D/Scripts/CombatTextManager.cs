using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatTextManager : MonoBehaviour
{
    private static CombatTextManager instance;

    public GameObject textPrefab;
    public GameObject sct { get; private set; }

    public RectTransform canvasTransform;

    public static CombatTextManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<CombatTextManager>();
            }
            return instance;
        }
    }

    public GameObject CreateText( Vector3 position, Vector3 direction, float speed, float timer, float fadeTime, string text, Color color, CombatText.TextType textType)
    {
        sct = Instantiate(textPrefab, position, Quaternion.identity);

        sct.transform.position = position + new Vector3(0,.5f, 0);
        sct.transform.SetParent(canvasTransform);
        sct.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        sct.GetComponent<CombatText>().Initialize(speed, direction, fadeTime, timer, textType);
        sct.GetComponent<Text>().text = text;
        sct.GetComponent<Text>().color = color;
        return sct;
    }
}
