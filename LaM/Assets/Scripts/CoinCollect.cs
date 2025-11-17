using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class CoinCollect : MonoBehaviour
{
    private int coins = 0;
    public TMP_Text coinText;
    private GameObject[] allCoins;
    public GhostController ghost;
    private bool coinChecked;
    private float dificulty;
    private int deathToll;

    private void Start()
    {
        coins = PlayerPrefs.GetInt("coins");
        allCoins = GameObject.FindGameObjectsWithTag("Coin");

        deathToll = coins % 10;
        coins = coins - deathToll;

        dificulty = coins / 10;
        dificulty = (float)Math.Ceiling(dificulty);
        ghost.speed = 3f + dificulty;
    }
    private void Update()
    {
        UpdateUI();

        if(coins%10 == 0 && coinChecked)
        {
            foreach (GameObject coin in allCoins)
            {
                coin.SetActive(true);
            }
            ghost.speed += 1f;
            coinChecked = false;
        }

        if (coins >= 100)
        {
            Win();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            PlayerPrefs.SetInt("coins", coins);
            PlayerPrefs.Save();
            coinChecked = true;
            other.gameObject.SetActive(false);
        }
    }

    private void UpdateUI()
    {
        coinText.text = coins + "/100 ";
    }

    public void Win()
    {
        SceneManager.LoadScene("Win Screen");
    }

    public int CoinScore()
    {
        return coins;
    }

    
    

}
