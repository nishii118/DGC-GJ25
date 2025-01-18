using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [Header("Element")]
    private float currentScore;
    private int highScore;
    [SerializeField] private float scorePerSecond = 1f;
    //[SerializeField] private TextMeshProUGUI scoreTxt;
    //[SerializeField] private TextMeshProUGUI bestcoreTxt;

    private void Start()
    {
        currentScore = 0;
        highScore = 0;
    }
    void Update()
    {
        // Tăng điểm dựa trên thời gian trôi qua
        currentScore += scorePerSecond * Time.deltaTime;
        Messenger.Broadcast<int>(EventKey.OnChangeScore, Mathf.FloorToInt(currentScore));
        // Hiển thị điểm số trên màn hình (chỉ để kiểm tra)
        // Debug.Log("Score: " + Mathf.FloorToInt(currentScore));
    }
    private void OnEnable()
    {
        // Messenger.AddListener<int>(EventKey.ADDSCORE, AddScore);
        // Messenger.AddListener(EventKey.UPDATEHIGHTSCORE, UpdateHighScore);
        Messenger.AddListener(EventKey.ONRESETCURRENTSCORE, OnResetCurrentScore);

    }

    private void OnDisable()
    {
        // Messenger.RemoveListener<int>(EventKey.ADDSCORE, AddScore);
        // Messenger.RemoveListener(EventKey.UPDATEHIGHTSCORE, UpdateHighScore);
        Messenger.RemoveListener(EventKey.ONRESETCURRENTSCORE, OnResetCurrentScore);
    }

    // private void AddScore(int score)
    // {
    //     currentScore += score;
    //     Messenger.Broadcast(EventKey.OnChangeScore);

    // }

    // private void UpdateHighScore()
    // {
    //     highScore = Mathf.Max(currentScore, highScore);
    //     Messenger.Broadcast(EventKey.OnChangeHighScore);

    //     //UpdateCurrentScore();
    // }

    // private void UpdateCurrentScore()
    // {
    //     currentScore = 0;

    //     Messenger.Broadcast(EventKey.OnChangeScore);
    // }

    // public int GetCurrentScore()
    // {
    //     return currentScore;
    // }

    // public int GetHighScore()
    // {
    //     return highScore;
    // }

    private void OnResetCurrentScore()
    {
        currentScore = 0;
        Messenger.Broadcast(EventKey.OnChangeScore);
    }

    public float GetCurrentScore() {
        return currentScore;
    }
}