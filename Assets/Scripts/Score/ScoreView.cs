using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTxt;
    // [SerializeField] TextMeshProUGUI highscoreTxt;
    void Start()
    {

        if (scoreTxt != null)
        {
            OnChangeScore(Mathf.FloorToInt(ScoreManager.Instance.GetCurrentScore()));
        }
        // if (highscoreTxt != null)
        // {
        //     OnChangeHighScore();
        // }
    }
    private void OnEnable()
    {
        
        Messenger.AddListener<int>(EventKey.OnChangeScore, OnChangeScore);
        // Messenger.AddListener(EventKey.OnChangeHighScore, OnChangeHighScore);
    }

    // private void OnChangeHighScore()
    // {
    //     if (highscoreTxt != null)
    //     {
    //         highscoreTxt.SetText(ScoreManager.Instance.GetHighScore().ToString());
    //     }
    // }
    private void OnChangeScore(int currentScore)
    {
        // Debug.Log("score mang")
        scoreTxt.SetText(currentScore.ToString());
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<int>(EventKey.OnChangeScore, OnChangeScore);
        // Messenger.RemoveListener(EventKey.OnChangeHighScore, OnChangeHighScore);
    }

}