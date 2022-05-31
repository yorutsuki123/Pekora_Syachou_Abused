using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch1Plot : MonoBehaviour
{
    [Header("UI組件")]
    public Text TextLabel;
    public Image Pekora,Moona;
    public Image Name;

    [Header("文本文件")]
    public TextAsset TextFile;

    [Header("立繪")]
    public Sprite PekoraBright,PekoraDark,MoonaBright,MoonaDark;
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
        print(TextList[index]);

        /*switch (TextList[index])
        {
            case "Pekora":
                Pekora.sprite = PekoraBright;
                Moona.sprite = MoonaDark;
                Name.sprite = PekoraName;
                break;

            case "Moona":
                Pekora.sprite = PekoraDark;
                Moona.sprite = MoonaBright;
                Name.sprite = MoonaName;
                break;
        }*/

        if (TextList[index][0] == 'P')
        {
            Pekora.sprite = PekoraBright;
            Moona.sprite = MoonaDark;
            Name.sprite = PekoraName;
            index++;
        }

        if (TextList[index][0] == 'M')
        {
            Pekora.sprite = PekoraDark;
            Moona.sprite = MoonaBright;
            Name.sprite = MoonaName;
            index++;
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