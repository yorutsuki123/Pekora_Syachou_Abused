using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StartButton : MonoBehaviour
{

    public GameObject tutor;

    private void Start()
    {
        noIntroduction();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        PlayerStatus.createEmpty();
        SceneManager.LoadScene("Chapter1");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("離開遊戲");
    }

    public void Introduction()
    {
        tutor.SetActive(true);
    }

    public void noIntroduction()
    {
        tutor.SetActive(false);
    }
}