
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class GameManager : Singleton<GameManager>
{
    void OnEnable()
    {
        Messenger.AddListener(EventKey.RestartGame, ReloadCurrentScene);
        Messenger.AddListener(EventKey.ENDGAME, ProcessEndGame);
    }

    void OnDisable()
    {
        Messenger.RemoveListener(EventKey.RestartGame, ReloadCurrentScene);
        Messenger.RemoveListener(EventKey.ENDGAME, ProcessEndGame);
    }
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log(currentScene.name);
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1;
    }

    public void ProcessEndGame()
    {
        PanelManager.Instance.OpenPanel("BlurPanel");

        PanelManager.Instance.OpenPanel("GameoverPanel");
    }
}