using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    Root root;

    float speed_x = 0.04f;

    public int score;
    public int bestScore;

    public Text[] txtScore;
    public Text txtBestScore;
    public GameObject gameOverPanel;

    private void Awake()
    {
        bestScore = PlayerPrefs.GetInt("bestScore");
    }

    private void Start()
    {
        root = Camera.main.GetComponent<Root>();
    }

    private void FixedUpdate()
    {
        if (root.isStarted == true)
        {
            this.transform.Translate(speed_x, 0, 0);
        }

        if (this.transform.position.x >= 1.25f || this.transform.position.x <= -1.25f)
        {
            speed_x *= -1;
        }

        for (int i = 0; i < txtScore.Length; i++)
        {
            txtScore[i].text = "you scored: " + score;
        }

        txtBestScore.text = "best score: " + bestScore;

        if (Input.GetKeyDown("space"))
        {
            Control();
        }
    }

    public void Control()
    {
        if (this.transform.position.x <= 1.15f && this.transform.position.x >= -1.15f)
        {
            speed_x *= -1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("bonus"))
        {
            Destroy(other.gameObject);
            score += Random.Range(10, 30);
        }
        if (other.name.Contains("enemy"))
        {
            Destroy(other.gameObject);
            Death();
            root.diesCount++;
            if (score >= 100)
            {
                root.enableTimerRemaining = true;
                root.continuePlayingPanel.SetActive(true);
                root.timerRemaining = 5;
            }
            else
            {
                gameOverPanel.SetActive(true);
            }
        }
    }

    public void Death()
    {
        root.isStarted = false;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestScore", bestScore);
        }
    }
}
