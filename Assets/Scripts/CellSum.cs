using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CellSum : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textSum;
    [SerializeField] public TextMeshProUGUI textNowSum;
    public int sum = 0, nowSum = 0;
    int choosed = 0;
    void Update()
    {
        textNowSum.text = nowSum.ToString();
        if(nowSum > 0) 
        {
            if(choosed == 0)
                textNowSum.gameObject.SetActive(true);
            choosed = 1;
        }
        else 
        {
            textNowSum.gameObject.SetActive(false);
        }
    }
}
