using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using SceneTransition;

// ласс, который отвечает за прив€зку кнопок, которые мен€ют сцены и открывают меню
public class ButtonStart : MonoBehaviour
{

    public void StartGame()
    {
        SceneTransition.SceneTransition.SwitchToScene("Game");
    }

    public void CloseGameScene()
    {
        if (SceneManager.GetActiveScene().name == "Game")
            SceneTransition.SceneTransition.SwitchToScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
