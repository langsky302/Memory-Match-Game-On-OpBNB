using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeoutDialog : Dialog
{
    public void BackToMenu(){
        if (SceneController.Ins)
        SceneController.Ins.LoadCurrentScene();
    }

    public void Replay() {
        SceneManager.sceneLoaded += OnSceneLoadedEvent;
        if (SceneController.Ins)
        SceneController.Ins.LoadCurrentScene();
    }

    private void OnSceneLoadedEvent(Scene scene, LoadSceneMode mode) {
        if(GameManager.Ins)
        GameManager.Ins.PlayGame();

          SceneManager.sceneLoaded -= OnSceneLoadedEvent;
    }
}
