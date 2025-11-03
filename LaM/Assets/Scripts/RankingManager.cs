using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Ranking
{
    public List<PlayerData> players = new List<PlayerData>();
}

public class RankingManager : MonoBehaviour
{
    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/ranking.json";
    }
    public void UpdateRanking(PlayerData newPlayer)
    {
        Ranking ranking = LoadRanking();

        PlayerData existingPlayer = ranking.players.Find(p => p.playerName == newPlayer.playerName);

        if (existingPlayer != null)
        {
            if (newPlayer.coins > existingPlayer.coins)
            {
                existingPlayer.coins = newPlayer.coins;
            }
        }
        else
        {
            ranking.players.Add(newPlayer);
        }

        ranking.players.Sort((a, b) => b.coins.CompareTo(a.coins));

        SaveRanking(ranking);
    }

    public void SaveRanking(Ranking ranking)
    {
        string json = JsonUtility.ToJson(ranking, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Ranking salvo em: " + filePath);
    }

    public Ranking LoadRanking()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<Ranking>(json);
        }
        else
        {
            return new Ranking();
        }
    }
}
