using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    BoardManager boardManager;
    private int boardSize, currlevel;
    public static bool hasGameFinished, turnHint, banChoose;
    private Cell[,] cells;
    public Animator animTransfer;
    bool IsPen = true; 
    LevelData currLevelData;
    public TextMeshProUGUI levelText;
    public GameObject Board8Cell, Board7Cell, Board6Cell, Board5Cell, Board4Cell, WinGameUI, ImageWin;
    public Camera captureCamera;
    public RenderTexture renderTexture;
    private void Awake()
    {
        turnHint = hasGameFinished = banChoose = false;
        currlevel = PlayerPrefs.GetInt("currLevel", 1);
        levelText.text = "Level " + currlevel.ToString();
        currLevelData = Resources.Load<LevelData>("Data/LevelGenerator/Level/Level " + currlevel.ToString());

        GameObject boardPrefab = Resources.Load<GameObject>("Prefab/Board"+currLevelData.rows.ToString()+"Cell");
        boardManager = Instantiate(boardPrefab).GetComponent<BoardManager>();

        boardSize = boardManager.BOARD_SIZE;
        RequestNewBoard(currLevelData);
    }
    
    private void RequestNewBoard(LevelData levelData)
    {
        int k = 0;
        for(int i = 0; i < levelData.rows; i++)
        {
            for(int j = 0; j < levelData.columns; j++)
            {
                boardManager.cellInBoard[k].value = levelData.board[i].column[j];
                boardManager.cellInBoard[k].IsAnsNum = levelData.board[i].column2[j]; 
                k++;
                // Debug.Log(levelData.board[i].column[j]);
            }
        }
    }
    void Start()
    {
        Hp.hp = SaveSystem.hp;
        for(int i = 0; i < boardSize; i++)
        {
            for(int j = 0; j < boardSize; j++)
            {
                if(SaveSystem.saveObj[i, j] == true)
                {
                    boardManager.cells[i,j].Select();
                    boardManager.cells[i,j].isChoosed = true;
                    if(boardManager.cells[i,j].IsAnsNum)
                    {
                        boardManager.cellsSumRow[i].nowSum += boardManager.cells[i,j].value;
                        boardManager.cellsSumCol[j].nowSum += boardManager.cells[i,j].value;
                    }
                }
            }
        }
    }
    void Update()
    {   
        if(hasGameFinished)
        {
            Capture();
            StartCoroutine(GameWinBoard());
        }
        if(hasGameFinished || !Input.GetMouseButtonDown(0)) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        Cell tempCell;
        if(!(hit
            && hit.collider.TryGetComponent(out tempCell)
            && !tempCell.isChoosed
            ))
        {
            return;
        }
        if(!banChoose)
            if(IsPen)
            {
                if(tempCell.IsAnsNum || turnHint)
                {
                    tempCell.Select();
                    tempCell.isChoosed = true;
                    int i = tempCell.row;
                    int j = tempCell.col;
                    SaveSystem.saveObj[i,j] = true;
                    boardManager.cellsSumRow[i].nowSum += boardManager.cells[i,j].value;
                    boardManager.cellsSumCol[j].nowSum += boardManager.cells[i,j].value;
                    turnHint = false;
                }
                else 
                {
                    tempCell.Wrong();
                    Hp.hp--;
                    SaveSystem.hp = Hp.hp;
                }
            }
            else 
            {
                if(!tempCell.IsAnsNum || turnHint)
                {
                    tempCell.Select();
                    tempCell.isChoosed = true;
                    SaveSystem.saveObj[tempCell.row,tempCell.col] = true;
                    SaveSystem.ChangeSaveObj();
                    turnHint = false;
                }
                else 
                {
                    tempCell.Wrong();
                    Hp.hp--;
                    SaveSystem.hp = Hp.hp;
                }
            }
        SaveSystem.ChangeSaveObj();
    }
    public void OnClickPen()
    {
        if(!IsPen)
        {
            IsPen = true;
            animTransfer.Play("TransferPenClick");
        }
    }
    public void OnClickErase()
    {
        if(IsPen)
        {
            IsPen = false;
            animTransfer.Play("TransferEraseClick");
        }
    }
    IEnumerator GameWinBoard()
    {
        yield return new WaitForSeconds(1f);
        WinGameUI.SetActive(true);
        PlayerPrefs.SetInt("currLevel", currlevel+1);
        SaveSystem.ResetSave();
    }
    public void LoadSaveScene()
    {
        SaveSystem.hp = 1;
        SaveSystem.ChangeSaveObj();
        SceneManager.LoadScene("Scenes/GamePlay");
    }
    public void LoadNewScene()
    {
        SaveSystem.ResetSave();
        SceneManager.LoadScene("Scenes/GamePlay");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void Capture()
    {
        // Thiết lập Render Texture cho camera
        captureCamera.targetTexture = renderTexture;

        // Render cảnh
        RenderTexture.active = renderTexture;
        captureCamera.Render();

        // Lấy texture từ Render Texture
        Texture2D temp = new Texture2D(renderTexture.width, renderTexture.height);
        temp.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        temp.Apply();

        Sprite screenshotSprite = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), new Vector2(0.5f, 0.5f));
        ImageWin.GetComponent<Image>().sprite = screenshotSprite;

        // Giải phóng Render Texture
        RenderTexture.active = null;
    }
}
