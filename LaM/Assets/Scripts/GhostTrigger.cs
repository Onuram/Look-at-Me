using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostTrigger : MonoBehaviour
{
    private string filePath;
    public CoinCollect coinCollect;
    public TMP_Text lifeText;
    private int coinCount;
    public RankingManager rankingManager;
    public GhostController ghost;
    private int health;
    private bool alreadyHit = false;


    private void Start()
    {
        health = 3;
        filePath = Application.persistentDataPath + "/playerdata.json";
    }

    private void Update()
    {
        coinCount = coinCollect.CoinScore();
        if (coinCount >= 100)
        {
            PlayerData data = LoadGame();
            SaveGame(data);
            rankingManager.UpdateRanking(data);
            SceneManager.LoadScene("Win Screen");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (alreadyHit) return;

        if (other.CompareTag("Player"))
        {
            alreadyHit = true;
            health--;
            UpdateUI();

            if (health <= 0)
            {
                coinCount = coinCollect.CoinScore();
                PlayerData data = LoadGame();
                PlayerPrefs.SetFloat("ghostSpeed", ghost.speed);
                SaveGame(data);
                rankingManager.UpdateRanking(data);

                SceneManager.LoadScene("Lose Screen");
            }
            else
            {
                StartCoroutine(ResetHit());
            }
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

    private void UpdateUI()
    {
        lifeText.text = "Vidas: " + health + "/3 ";
    }

    private IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(1f);
        alreadyHit = false;
    }
}
