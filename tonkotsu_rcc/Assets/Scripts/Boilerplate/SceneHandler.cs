using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    //This definetly should be less dependend on correct build settings, could be changed to Singleton so we don't need statics / even better would be a different public access

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void LoadCredits()
    {
        SceneManager.LoadScene(1);
    }

    public static void LoadIntro()
    {
        SceneManager.LoadScene(0);
    }

    public static void LoadLevelWithIndex(int i)
    {
        SceneManager.LoadScene(i);
    }

    public static void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public static void LoadNextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }
}