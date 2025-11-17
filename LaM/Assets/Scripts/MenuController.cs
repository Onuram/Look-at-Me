using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int coins;
    public int ghostSpeed;
}

public class MenuController : MonoBehaviour
{
    public TMP_InputField username;
    private string filePath;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        filePath = Application.persistentDataPath + "/playerdata.json";
    }

    public void BtnStart()
    {
        SaveGame();
        SceneManager.LoadScene("Gameplay");
    }
    public void BtnContinue()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void BtnSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void BtnCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void BtnMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void BtnRanking()
    {
        SceneManager.LoadScene("Rank Screen");
    }

    public void BtnExit()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        PlayerData data = new PlayerData
        {
            playerName = username.text,
        };
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        
        Debug.Log("Jogo salvo em: " + filePath);
    }
}
