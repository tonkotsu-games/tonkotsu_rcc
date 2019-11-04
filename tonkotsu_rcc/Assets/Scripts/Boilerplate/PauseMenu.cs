using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        GameState.Instance.TryChangeState(GameState.GameStates.Play);
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        GameState.Instance.TryChangeState(GameState.GameStates.Menu);
    }

    public void Quit()
    {
        GameState.Instance.TryChangeState(GameState.GameStates.Quit);
    }
}