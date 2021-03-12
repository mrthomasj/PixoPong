using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform _player1;
    [SerializeField]
    private Transform _player2;
    [SerializeField]
    private Transform _ball;
    [SerializeField]
    private Text p1ScoreDisplay;
    [SerializeField]
    private Text p2ScoreDisplay;
    [SerializeField]
    private Text countdownDisplay;

    private GameObject menu;

    private int _scoreToWin = 5;

    public bool _justScored;

    public int playerOneScore = 0;
    public int playerTwoScore = 0;
    public bool gameRunning = false;

    private int countdownTimer = 3;

    private void Start()
    {
        _ball.gameObject.SetActive(false);
        menu = GameObject.FindGameObjectWithTag("Menu");
        _justScored = false;
    }

    private void Update()
    {
        CheckScore();
    }

    public void DestroySound()
    {
        var sound = GameObject.Find("Sound");
        if (!sound.GetComponent<AudioSource>().isPlaying)
        {
            Destroy(sound);
        }
        
    }

    private void CheckScore()
    {
        if (_justScored)
        {
            if(playerOneScore >= _scoreToWin)
            {
                gameRunning = false;
                var winMessage = menu.transform.Find("WinnerMessage").GetComponent<Text>();
                winMessage.text = $"Congratulations!\nYou won!!!";
                winMessage.gameObject.SetActive(true);
                var playBtn = menu.transform.Find("PlayBtn");
                var playAgainBtn = menu.transform.Find("PlayAgainBtn");
                playBtn.gameObject.SetActive(false);
                playAgainBtn.gameObject.SetActive(true);
                menu.SetActive(true);
            }
            else if(playerTwoScore >= _scoreToWin)
            {
                gameRunning = false;
                var winMessage = menu.transform.Find("WinnerMessage").GetComponent<Text>();
                winMessage.text = $"You lost...\nBetter luck next time, pal!";
                winMessage.gameObject.SetActive(true);
                var playBtn = menu.transform.Find("PlayBtn");
                var playAgainBtn = menu.transform.Find("PlayAgainBtn");
                playBtn.gameObject.SetActive(false);
                playAgainBtn.gameObject.SetActive(true);
                menu.SetActive(true);
            }
            else
            {
                ResetRound();
            }
        }
    }

    public void PlayAgain()
    {
        menu.SetActive(false);
        playerOneScore = 0;
        playerTwoScore = 0;
        SetScoreUI(1);
        SetScoreUI(2);
        ResetRound();

    }
    

    public void PlayGame()
    {
        menu.SetActive(false);
        Countdown();
    }

    private void GameStart()
    {

        gameRunning = true;

        _ball.gameObject.SetActive(true);

        _ball.GetComponent<Ball>().LaunchBall();
    }


    private IEnumerator CountdownToStart()
    {
        while(countdownTimer > 0)
        {
            Debug.Log($"Start in {countdownTimer}!");
            countdownDisplay.text = countdownTimer.ToString();

            yield return new WaitForSeconds(1f);

            countdownTimer--;
        }

        countdownDisplay.text = "GO!";

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);

        GameStart();
    }

    public void Countdown()
    {
        countdownDisplay.gameObject.SetActive(true);
        StartCoroutine("CountdownToStart");
    }


    public void ResetRound()
    {
        _justScored = false;
        _ball.gameObject.SetActive(false);

        gameRunning = false;

        countdownTimer = 3;

        _player1.localPosition = new Vector3(-62f, 0, 0);
        _player2.localPosition = new Vector3(62f, 0, 0);
        _ball.localPosition = new Vector3(0, Random.Range(-20f, 20f), 0);

        Countdown();
    }

    

    public void SetScoreUI(int reference)
    {
        if(reference == 1)
        {
            p1ScoreDisplay.text = playerOneScore.ToString();
        }
        else
        {
            p2ScoreDisplay.text = playerTwoScore.ToString();
        }
    }
}
