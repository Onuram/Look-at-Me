using System.IO;
using UnityEngine;
using TMPro;
using System.Text;

public class RankingController : MonoBehaviour
{
    public TMP_Text rankingTxt;
    private string filePath;
    private Ranking ranking;
    private StringBuilder sb = new StringBuilder();

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        filePath = Application.persistentDataPath + "/ranking.json";
        SetRanking();
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
    void SetRanking()
    {
        ranking = LoadRanking();
        if (ranking.players == null || ranking.players.Count == 0)
        {
            rankingTxt.text = "Nenhum jogador no ranking ainda.";
            return;
        }

        int count = Mathf.Min(5, ranking.players.Count);
        for (int i = 0; i < count; i++)
        {
            sb.AppendLine($"{i + 1}. {ranking.players[i].playerName} - {ranking.players[i].coins} moedas");
        }

        rankingTxt.text = sb.ToString();

    }

}
