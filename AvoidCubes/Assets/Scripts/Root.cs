using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Root : MonoBehaviour
{
    public GameObject cube;
    public GameObject bonus;

    Vector3 spawnPosition;

    float timerEnemy;
    float timerBonus;

    float bonusToScoreTimer;

    public bool isStarted = false;

    public GameObject palka;
    public GameObject continuePlayingPanel;

    public int diesCount;

    Hero hs;

    public bool enableTimerRemaining = false;
    public float timerRemaining = 5;

    public Text txtTimeRemaining;
    public Text txtBreakRecord;

    public GameObject playButton;

    private void Awake()
    {
        hs = GameObject.Find("hero").GetComponent<Hero>();
    }

    private void Start()
    {
        Advertisement.Initialize("3834751", false);
    }

    private void Update()
    {
        if (isStarted == true)
        {
            palka.SetActive(true);
        }

        if (isStarted == true)
        {
            timerEnemy += Time.deltaTime;
            timerBonus += Time.deltaTime;

            if (timerEnemy >= Random.Range(1, 3))
            {
                spawnPosition = new Vector3(Random.Range(-2.5f, 2.5f), 6, 0);
                GameObject c = Instantiate(cube);
                c.transform.position = spawnPosition;
                c.name = "enemy";
                timerEnemy = 0;
            }

            if (timerBonus >= Random.Range(3, 6))
            {
                spawnPosition = new Vector3(Random.Range(-2.5f, 2.5f), 6, 0);
                GameObject c = Instantiate(bonus);
                c.transform.position = spawnPosition;
                timerBonus = 0;
            }

            bonusToScoreTimer += Time.deltaTime;
            if (bonusToScoreTimer >= 1)
            {
                hs.score += 1;
                bonusToScoreTimer = 0;
            }
        }

        if (diesCount == Random.Range(3, 5))
        {
            if (Advertisement.IsReady("video"))
            {
                Advertisement.Show("video");
                
            }
            diesCount = 0;
        }

        if (enableTimerRemaining == true)
        {
            txtBreakRecord.text = "*" + (hs.bestScore - hs.score) + " points to break the record!";
            txtTimeRemaining.text = Mathf.RoundToInt(timerRemaining) + "";
            timerRemaining -= Time.deltaTime;

            if (timerRemaining < 5 && timerRemaining > 4.75f)
            {
                Destroy(GameObject.Find("enemy"));
            }

            if (timerRemaining <= 0)
            {
                hs.gameOverPanel.SetActive(true);
                continuePlayingPanel.SetActive(false);
                timerRemaining = 5;
                enableTimerRemaining = false;
            }
        }
    }

    public void StartGame(GameObject playButton)
    {
        isStarted = true;
        playButton.SetActive(false);
    }

    public void Restart()
    {
        hs.score = 0;
        isStarted = true;
        hs.gameOverPanel.SetActive(false);
    }

    public void ContinueGame()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
            diesCount = 0;
            isStarted = true;
            continuePlayingPanel.SetActive(false);
            playButton.SetActive(false);
            hs.gameOverPanel.SetActive(false);
            enableTimerRemaining = false;
        }
    }
}