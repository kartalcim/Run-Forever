using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool oyunBasladi;
    public GameObject gameOverPanel;
    public GameObject anaMenuPanel;
    public GameObject RekorPanel;
    public GameObject SayacPanel;
    public static int para;
    public static int puan;
    public Text ParaText;
    public Text MenuParaText;
    public Text PuanText;
    public Text MenuPuanText;
    public Text RekorPuanText;
    public AudioSource paraTopla;
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        oyunBasladi = false;
        para = 0;
        puan = 0;
        MenuPuanText.text = "Rekor: " + PlayerPrefs.GetInt("RekorPuan", puan);
        MenuParaText.text = "Altýn: " + PlayerPrefs.GetInt("ToplamPara", para);
    }

    // Update is called once per frame
    void Update()
    {
        PuanText.text = "Puan: " + puan;
        ParaText.text = "Altýn: " + para;
        if (gameOver)
        {
            Time.timeScale = 0;
            if (puan > PlayerPrefs.GetInt("RekorPuan", 0))
            {
                RekorPanel.SetActive(true);
                RekorPuanText.text = "Yeni Rekor: " + puan;
                PlayerPrefs.SetInt("RekorPuan", puan);
            }
            gameOverPanel.SetActive(true);            
        }
    }
    public void Baslat()
    {
        oyunBasladi = true;
        anaMenuPanel.SetActive(false);
        SayacPanel.SetActive(true);
        RekorPanel.SetActive(false);
    }
    public void Tekrar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Cikis()
    {
        Application.Quit();
    }
}
