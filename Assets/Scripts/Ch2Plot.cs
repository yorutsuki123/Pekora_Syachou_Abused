using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch2Plot : MonoBehaviour
{
    [Header("UI組件")]
    public Text TextLabel;
    public Image Pekora,Miko;
    public Image Name,Dialog;

    [Header("文本文件")]
    public TextAsset TextFile;

    [Header("立繪")]
    public Sprite PekoraBright,PekoraDark,MikoBright,MikoDark;
    public Sprite PekoraName,MoonaName;

    public int index;
    public float TextSpeed;
    bool TextFinished;
    bool QuickShow;

    List<string> TextList = new List<string>();

    void Awake()
    {
        GetSetFromFile(TextFile);
    }

    private void OnEnable()
    {
        TextFinished = true;
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && index == TextList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(TextFinished && !QuickShow)
            {
                StartCoroutine(SetTextUI());
            }
            else if(!TextFinished)
            {
                QuickShow = !QuickShow;
            }
        }   
    }

    void GetSetFromFile(TextAsset file)
    {
        TextList.Clear();
        index = 0;

        var LIneData = file.text.Split('\n');

        foreach (var Line in LIneData)
        {
            TextList.Add(Line);
        }
    }

    IEnumerator SetTextUI()
    {
        TextFinished = false;
        TextLabel.text = "";

        switch(TextList[index])
        {
            case "Pekora":
                Pekora.sprite = PekoraBright;
                Miko.sprite = MikoDark;
                Name.sprite = PekoraName;
                index++;
                break;

            case "Miko":
                Pekora.sprite = PekoraDark;
                Miko.sprite = MikoBright;
                Name.sprite = MoonaName;
                index++;
                break;
        }

        int Letter = 0;
        while (!QuickShow && Letter < TextList[index].Length -1)
        {
            TextLabel.text += TextList[index][Letter];
            Letter++;
            yield return new WaitForSeconds(TextSpeed);
        }

        TextLabel.text = TextList[index];
        QuickShow = false;
        TextFinished = true;
        index++;
    }
}