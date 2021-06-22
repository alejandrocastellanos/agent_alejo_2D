using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text levelCleared;
    public Text totalCoins;
    public Text catchCoins;
    private int totalCoinsInLevel;


    public void Start()

    {
        totalCoinsInLevel = transform.childCount;
    }

    public void Update()
    {
        AllFruitsCollected();
        totalCoins.text = totalCoinsInLevel.ToString();
        catchCoins.text = transform.childCount.ToString();
    }

    public void AllFruitsCollected()
    {
        if (transform.childCount == 0)
        {
            levelCleared.gameObject.SetActive(true);
            PlayerPrefs.DeleteAll();
            Invoke("ChangeScene", 2);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
