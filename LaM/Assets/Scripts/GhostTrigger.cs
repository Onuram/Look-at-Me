using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostTrigger : MonoBehaviour
{
    private string filePath;
    public CoinCollect coinCollect;
    private int coinCount;
    public RankingManager rankingManager;
    public GhostController ghost;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/playerdata.json";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coinCount = coinCollect.CoinScore();
            PlayerData data = LoadGame();
            PlayerPrefs.SetFloat("ghostSpeed", ghost.speed);
            SaveGame(data);

            rankingManager.UpdateRanking(data);

            SceneManager.LoadScene("Lose Screen");
        }
    }

    public void SaveGame(PlayerData data)
    {
        data.coins = coinCount;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Jogo salvo em: " + filePath);
    }
    
    public PlayerData LoadGame()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            Debug.Log($"Dados carregados - Nome: {data.playerName}, Moedas: {data.coins}");
            return data;
        }
        else
        {
            Debug.LogWarning("Arquivo de não encontrado!");
            return null;
        }
    }
}
