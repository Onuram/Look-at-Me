using UnityEngine;

public class ResetScore : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("coins", 0);
        PlayerPrefs.SetFloat("ghostSpeed", 3);
        PlayerPrefs.Save();
    }

}
