using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuffStatusNumHandler : MonoBehaviour
{
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
        }
        if (player != null)
        {
            gameObject.GetComponent<Text>().text = "¼W¯q®ÄªG" + Math.Pow(2, player.GetComponent<PlayerController>().buff).ToString() + "­¿";
        }
    }
}
