using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{

    public GameObject pauseUI;
    [SerializeField] bool isPause;
    bool isEscInUse;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        pauseUI.SetActive(false);
    }

    public void contiGame()
    {
        isPause = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void showPause()
    {
        isPause = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Â÷¶}¹CÀ¸");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Cancel") > 0)
        {
            if (!isEscInUse)
            {
                if (isPause)
                    contiGame();
                else
                    showPause();
            }
            isEscInUse = true;
        }
        else
            isEscInUse = false;
    }
}
