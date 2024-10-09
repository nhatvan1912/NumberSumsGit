using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hint : MonoBehaviour
{
    public static int hintRemain;
    public TextMeshProUGUI hintRemainText;
    bool isTurningHint;
    void Start()
    {
        hintRemain = PlayerPrefs.GetInt("hintRemain", 3);
        hintRemainText.text = hintRemain.ToString();
    }
    void Update()
    {
        if(!GameManager.turnHint)
        {
            isTurningHint = false;
        }
    }
    public void TurnHint()
    {
        if (hintRemain > 0 && !isTurningHint)
        {
            hintRemain--;
            Debug.Log(hintRemain);
            PlayerPrefs.SetInt("hintRemain", hintRemain);
            hintRemainText.text = hintRemain.ToString();
            isTurningHint = true;
            GameManager.turnHint = true;
        }
        if(hintRemain == 0)
        {
            hintRemainText.text = "Ad";
        }
    }
}
