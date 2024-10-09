using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardManager : MonoBehaviour
{
    public int BOARD_SIZE ;
    public List<Cell> cellInBoard;
    public Cell[,] cells;
    public List<CellSum> cellsSumCol, cellsSumRow;
    bool[] checkRow, checkCol;
    void Awake()
    {
        cells = new Cell[BOARD_SIZE, BOARD_SIZE];
        int k = 0;
        for(int i = 0; i < BOARD_SIZE; i++)
        {
            for(int j = 0; j < BOARD_SIZE; j++)
            {
                cellInBoard[k].row = i;
                cellInBoard[k].col = j;
                cells[i, j] = cellInBoard[k];
                k++;
            }
        }
    }
    void Start()
    {
        checkCol = new bool[BOARD_SIZE];
        checkRow = new bool[BOARD_SIZE];
        for(int i = 0; i < BOARD_SIZE; i++)
        {
            for(int j = 0; j < BOARD_SIZE; j++)
            {
                if(cells[i, j].IsAnsNum)
                {
                    // Debug.Log("Cong");
                    cellsSumRow[i].sum += cells[i, j].value;
                }
                if(cells[j, i].IsAnsNum)
                {
                    cellsSumCol[i].sum += cells[j, i].value;
                }
            }
            cellsSumRow[i].textSum.text = cellsSumRow[i].sum.ToString();
            cellsSumCol[i].textSum.text = cellsSumCol[i].sum.ToString();
            checkCol[i] = checkRow[i] = false;
        }
    }
    void Update()
    {
        int checkGameOver = 0;
        for(int i = 0; i < BOARD_SIZE; i++)
        {
            int check1 = 0, check2 = 0;
            for(int j = 0; j < BOARD_SIZE; j++)
            {
                if(!cells[i,j].isChoosed)
                {
                    checkGameOver = 1;
                    check1 = 1;
                }
                if(!cells[j,i].isChoosed)
                {
                    check2 = 1;
                }
            }
            if(check1 == 0) 
            {
                if(!checkRow[i])
                {
                    StartCoroutine(RowDone(i));
                    checkRow[i] = true;
                }
                cellsSumRow[i].gameObject.SetActive(false);
            }
            if(check2 == 0)
            {
                if(!checkCol[i])
                {
                    StartCoroutine(ColDone(i));
                    checkCol[i] = true;
                }
                cellsSumCol[i].gameObject.SetActive(false);
            }
        }
        if(checkGameOver == 0) 
            GameManager.hasGameFinished = true;
    }
    IEnumerator RowDone(int i)
    {
        for(int j = 0; j < BOARD_SIZE; j++)
        {
            cells[i,j].RowDone();
            yield return new WaitForSeconds(0.15f);
        }
    }
    IEnumerator ColDone(int i)
    {
        for(int j = 0; j < BOARD_SIZE; j++)
        {
            cells[j,i].ColDone();
            yield return new WaitForSeconds(0.15f);
        }
    }
}
