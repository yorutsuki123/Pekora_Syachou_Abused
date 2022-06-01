using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkNextScene : MonoBehaviour
{
    public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player")
            return;
        GameObject.FindWithTag("MainCamera").GetComponent<GameRule>().saveStatus();
        GameObject.FindWithTag("MainCamera").GetComponent<GameRule>().isEnd = true;
        SceneManager.LoadScene(nextScene);
    }
}
