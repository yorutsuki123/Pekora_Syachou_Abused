using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{

    public Image Cover;
    public Sprite Picture;

    void Start()
    {
        Cover.sprite = Picture;
    }

}
