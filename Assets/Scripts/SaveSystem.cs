using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static readonly string SAVE_FOLDER =  Application.dataPath + "/Resources/Data/Saves/";
    private static SaveObject saveObject;
    BoardManager boardManager;
    private static int boardSize;
    private Cell[,] cells;
    public static bool[,] saveObj;
    public static int hp;
    void Start()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
        boardManager = GameObject.FindWithTag("Board").GetComponent<BoardManager>();
        boardSize = boardManager.BOARD_SIZE;
        saveObj = new bool[boardSize, boardSize];
        // Debug.Log(boardSize);
        FileInfo fileInfo = new FileInfo(SAVE_FOLDER + "/savejson.txt");
        if (fileInfo.Length != 0)
        {
            saveObject = JsonConvert.DeserializeObject<SaveObject>(Load());
        }
        else{
            saveObject = new SaveObject();
            saveObject.isChoosed = new bool[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    saveObject.isChoosed[i, j] = false;
                }
            }
            saveObject.hp = Hp.hp;
            string json = JsonConvert.SerializeObject(saveObject);
            Save(json);
        }
        saveObj = saveObject.isChoosed;
        hp = saveObject.hp;
    } 
    public static void ChangeSaveObj()
    {
        saveObject.isChoosed = saveObj;
        saveObject.hp = hp;
        string json = JsonConvert.SerializeObject(saveObject);
        Save(json);
    }
    public static void Save(string saveString)
    { 
        File.WriteAllText(SAVE_FOLDER + "/savejson.txt", saveString);
        // Debug.Log(saveString);
    }
    public static void ResetSave()
    {
        File.WriteAllText(SAVE_FOLDER + "/savejson.txt", "");
    }
    public string Load()
    {
        string saveString = File.ReadAllText(SAVE_FOLDER + "/savejson.txt");
        return saveString;
    }
    
}
public class SaveObject
{
    public bool[,] isChoosed;
    public int hp;
    // public bool isChoosed ;
}