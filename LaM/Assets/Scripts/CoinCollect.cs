using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinCollect : MonoBehaviour
{
    private int coins = 0;
    public TMP_Text coinText;
    private GameObject[] allCoins;
    public GhostController ghost;
    private bool coinChecked;

    private void Start()
    {
        coins = PlayerPrefs.GetInt("coins");
        allCoins = GameObject.FindGameObjectsWithTag("Coin");
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

        if (coins == 100)
        {
            Win();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            //Debug.Log("Moeda coletada");
            PlayerPrefs.SetInt("coins", coins);
            PlayerPrefs.Save();
            coinChecked = true;
            other.gameObject.SetActive(false);
        }
    }

    private void UpdateUI()
    {
        coinText.text = "Moedas: " + coins;
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
