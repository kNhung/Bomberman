using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void LoadLevel_1(){
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadLevel_2(){
        SceneManager.LoadSceneAsync(2);
    }

    public void LoadLevel_3(){
        SceneManager.LoadSceneAsync(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
