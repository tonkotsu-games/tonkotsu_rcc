using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject menuObject = null;
    [SerializeField] GameObject levelSelection = null;

    private void Start()
    {
        GameState.Instance.TryChangeState(GameState.GameStates.Menu);

        menuObject.SetActive(true);
        levelSelection.SetActive(false);
    }

    public void LevelSelection()
    {
        menuObject.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void BackToMenu()
    {
        menuObject.SetActive(true);
        levelSelection.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        GameState.Instance.TryChangeState(GameState.GameStates.Quit);
    }

    public void OpenCredits()
    {
        GameState.Instance.TryChangeState(GameState.GameStates.Credits);
    }

    public void LoadLevel(int index)
    {
        index += 3;
        GameState.Instance.TryChangeState(GameState.GameStates.Play);
        SceneHandler.LoadLevelWithIndex(index);
    }
}
