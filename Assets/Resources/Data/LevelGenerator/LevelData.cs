using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
[System.Serializable]
public class LevelData : ScriptableObject
{
    [System.Serializable]
    public class Row{
        public int[] column;
        public bool[] column2;
        private int _size = 0;
        public Row(){}
        public Row(int size)
        {
            CreateRow(size);
        }
        public void CreateRow(int size)
        {
            _size = size;
            column = new int[_size];
            column2 = new bool[_size];
            ClearRow();
        }
        public void ClearRow()
        {
            for (int i = 0; i < _size; i++)
            {
                column[i] = 0;
                column2[i] = false;
            }
        }
    }
    public int columns = 0;
    public int rows = 0;
    public Row[] board;
    public void Clear()
    {
        for (int i = 0; i < rows; i++)
        {
            board[i].ClearRow();
        }
    }
    public void CreateNewBoard()
    {
        board = new Row[rows];
        for(var i=0; i< rows; i++)
        {
            board[i] = new Row(columns);
        }
    }
}
