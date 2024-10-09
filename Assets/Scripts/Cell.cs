using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Cell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] public int value, row, col;
    public GameObject circle, circleWrong, coatingRow, coatingCol;
    public Animator animator;
    public bool IsAnsNum, isChoosed;
    void Awake()
    {
        isChoosed = false;
    }
    void Start()
    {
        valueText.text = value.ToString();
    }
    public void Select()
    {
        if(IsAnsNum)
        {
            circle.SetActive(true);
            animator.Play("CellTrue");
        }
        else
        {
            animator.Play("CellErase");
            StartCoroutine(SetCellFalse());
        }
    }
    IEnumerator SetCellFalse()
    {
        yield return new WaitForSeconds(.5f);
        valueText.gameObject.SetActive(false);
    }
    public void Wrong()
    {
        animator.Play("CellWrong");
        circleWrong.SetActive(true);
        StartCoroutine(SetCircleWrong());
    }
    IEnumerator SetCircleWrong()
    {
        yield return new WaitForSeconds(1.1f);
        circleWrong.SetActive(false);
    }
    public void RowDone()
    {
        coatingRow.SetActive(true);
        animator.Play("RowDone");
        StartCoroutine(SetRowFalse());
    }
    IEnumerator SetRowFalse()
    {
        yield return new WaitForSeconds(0.15f);
        coatingRow.SetActive(false);
    }
    public void ColDone()
    {
        coatingCol.SetActive(true);
        animator.Play("ColDone");
        StartCoroutine(SetColFalse());
    }
    IEnumerator SetColFalse()
    {
        yield return new WaitForSeconds(0.15f);
        coatingCol.SetActive(false);
    }
}
