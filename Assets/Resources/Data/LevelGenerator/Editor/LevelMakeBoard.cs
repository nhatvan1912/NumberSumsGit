using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LevelData), false)]
[CanEditMultipleObjects]
[System.Serializable]
public class LevelMakeBoard : Editor
{
    private LevelData LevelDataInstance => target as LevelData;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ClearBoardButton();
        EditorGUILayout.Space();
        DrawColumnsInputFields();
        EditorGUILayout.Space();
        if(LevelDataInstance.board != null && LevelDataInstance.columns > 0 && LevelDataInstance.rows > 0)
        {
            DrawBoardTable();
            DrawAnsNum();
        }
        // if(LevelDataInstance.isAnsNum != null && LevelDataInstance.columns > 0 && LevelDataInstance.rows > 0)
        // {
        //     DrawAnsNum();
        // }
        serializedObject.ApplyModifiedProperties();
        if(GUI.changed)
        {
            EditorUtility.SetDirty(LevelDataInstance);
        }
        
    }

    private void ClearBoardButton()
    {
        if(GUILayout.Button("Clear Board"))
        {
            LevelDataInstance.Clear();
        }
    }
    private void DrawColumnsInputFields()
    {
        var columnsTemp = LevelDataInstance.columns;
        var rowsTemp = LevelDataInstance.rows;

        LevelDataInstance.columns = EditorGUILayout.IntField("Columns", LevelDataInstance.columns);
        LevelDataInstance.rows = EditorGUILayout.IntField("Rows", LevelDataInstance.rows);

        if((LevelDataInstance.columns != columnsTemp ||LevelDataInstance.rows != rowsTemp) && LevelDataInstance.columns > 0 && LevelDataInstance.rows > 0)
        {
            LevelDataInstance.CreateNewBoard();
        }
    }
    private void DrawAnsNum()
    {
        var tableStyle = new GUIStyle("box");
        tableStyle.padding = new RectOffset(10,10,10,10);
        
        var headerColumnStyle = new GUIStyle();
        headerColumnStyle.fixedWidth = 65;
        headerColumnStyle.alignment = TextAnchor.MiddleCenter;

        var rowStyle = new GUIStyle();
        rowStyle.fixedWidth = 35;
        rowStyle.alignment = TextAnchor.MiddleCenter;

        var dataFieldStyle = new GUIStyle();
        dataFieldStyle.normal.background = Texture2D.grayTexture;
        dataFieldStyle.onNormal.background = Texture2D.whiteTexture;

        for(var row = 0; row < LevelDataInstance.rows; row++)
        {
            EditorGUILayout.BeginHorizontal(headerColumnStyle);
            for(var column = 0; column < LevelDataInstance.columns; column++)
            {
                EditorGUILayout.BeginHorizontal(rowStyle);
                var data = EditorGUILayout.Toggle(LevelDataInstance.board[row].column2[column], dataFieldStyle);
                LevelDataInstance.board[row].column2[column] = data;
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void DrawBoardTable()
    {
        var tableStyle = new GUIStyle("box");
        tableStyle.padding = new RectOffset(10,10,10,10);
        
        var headerColumnStyle = new GUIStyle();
        headerColumnStyle.fixedWidth = 65;
        headerColumnStyle.alignment = TextAnchor.MiddleCenter;

        var rowStyle = new GUIStyle();
        rowStyle.fixedWidth = 35;
        rowStyle.alignment = TextAnchor.MiddleCenter;

        var dataFieldStyle = new GUIStyle();
        dataFieldStyle.normal.textColor = Color.white;
        dataFieldStyle.alignment = TextAnchor.MiddleCenter;

        for(var row = 0; row < LevelDataInstance.rows; row++)
        {
            EditorGUILayout.BeginHorizontal(headerColumnStyle);
            for(var column = 0; column < LevelDataInstance.columns; column++)
            {
                EditorGUILayout.BeginHorizontal(rowStyle);
                var data = EditorGUILayout.IntField(LevelDataInstance.board[row].column[column], dataFieldStyle);
                LevelDataInstance.board[row].column[column] = data;
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
