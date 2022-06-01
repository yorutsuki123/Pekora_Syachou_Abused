using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Chapter1");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("離開遊戲");  //Unity測試用
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}