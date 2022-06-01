using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpHandler : MonoBehaviour
{

    public Sprite full;
    public Sprite half;
    public Sprite none;
    public int num;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            gameObject.GetComponent<Image>().sprite = none;
        }
        if (player != null)
        {
            try
            {
                if (player.GetComponent<PlayerController>().hp >= num * 2)
                    gameObject.GetComponent<Image>().sprite = full;
                else if (player.GetComponent<PlayerController>().hp == num * 2 - 1)
                    gameObject.GetComponent<Image>().sprite = half;
                else
                    gameObject.GetComponent<Image>().sprite = none;
            }
            catch (System.Exception)
            {
                gameObject.GetComponent<Image>().sprite = none;
            }
            
        }
    }
}
