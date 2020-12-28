using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject menuPnl;
    public GameObject scorePnl;
    public GameObject settingPnl;
    public GameObject ContinueGamebtn;
    public GameObject StartNewGamebtn;
    public TextMeshProUGUI lastScore;
    public TextMeshProUGUI hightScore;
    private Player player;
    private int FullScore;

    public Text scoreCounter;

    private void Start()
    {
        Time.timeScale = 0;
        player = FindObjectOfType<Player>();
        lastScore.text = "Ваш последний результат: \n" + PlayerPrefs.GetInt("lastFullScore");
        hightScore.text = "Ваш лучший результат: \n" + PlayerPrefs.GetInt("hightFullScore");
    }

    
    public void Update()
    {
        scoreCounter.text = player.score.ToString();
    }

    public void FinishGame()
    {
        
        
        if (player.dead)
        {
            FullScore = player.score + player.dieZombieCounter;
        }
        if (!player.dead)
        {
            FullScore = player.score + player.dieZombieCounter + player.health + 500;
        }
        PlayerPrefs.SetInt("lastFullScore", FullScore);
        if (PlayerPrefs.GetInt("hightFullScore") < PlayerPrefs.GetInt("lastFullScore"))
        {
            PlayerPrefs.SetInt("hightFullScore",FullScore);
        }
        StartCoroutine(PlayerDieTime());
    }

    public void StartGame()
    { 
        menuPnl.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void StartNewGame()
    {
        SceneManager.LoadScene($"Location{SceneManager.GetActiveScene().buildIndex + 1}");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        menuPnl.SetActive(true);
        ContinueGamebtn.GetComponentInChildren<TextMeshProUGUI>().text = "Продолжить";
        StartNewGamebtn.SetActive(true);
    }
    
    public void SettingMenuOpen()
    {
        settingPnl.SetActive(true);
    }
    
    public void SettingMenuClose()
    {
        settingPnl.SetActive(false);
    }
    
    public void ScoreMenuOpen()
    {
        lastScore.text = "Ваш последний результат: \n" + PlayerPrefs.GetInt("lastFullScore");
        hightScore.text = "Ваш лучший результат: \n" + PlayerPrefs.GetInt("hightFullScore");
        
        scorePnl.SetActive(true);
    }
    
    public void ScoreMenuClose()
    {
        scorePnl.SetActive(false);
    }


    IEnumerator PlayerDieTime()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        menuPnl.SetActive(true);
        ContinueGamebtn.SetActive(false);
        StartNewGamebtn.SetActive(true);
        ScoreMenuOpen();
    }
}
