using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public Button levelButton, continueButton, startAgainButton;
    public TextMeshProUGUI levelText;
    public static readonly string SAVE_FOLDER =  Application.dataPath + "/Resources/Data/Saves/";
    public static bool IsContinue;
    void Start()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
        if(!File.Exists(SAVE_FOLDER + "/savejson.txt"))
        {
            File.Create(SAVE_FOLDER + "/savejson.txt");
        }
        FileInfo fileInfo1 = new FileInfo(SAVE_FOLDER + "/savejson.txt");
        if(fileInfo1.Length == 0)
        {
            levelButton.gameObject.SetActive(true);
            levelText.text = "Level " + PlayerPrefs.GetInt("currLevel", 1).ToString();
        }
        else 
        {
            continueButton.gameObject.SetActive(true);
            startAgainButton.gameObject.SetActive(true);
        }
    }
    public void LoadSaveScene()
    {
        SceneManager.LoadScene("Scenes/GamePlay");
    }
    public void LoadNewScene()
    {
        SaveSystem.ResetSave();
        SceneManager.LoadScene("Scenes/GamePlay");
    }
}
