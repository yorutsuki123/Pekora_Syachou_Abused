using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChEndPlot : MonoBehaviour
{
    
    [Header("文本文件")]
    public TextAsset TextFile;

    [Header("UI組件")]
    public GameObject TextName;
    public GameObject TextLabel;
    public GameObject LeftPicture,RightPicture;
    public GameObject Name,Dialog;
    public GameObject CG;

    [Header("圖片")]
    public Sprite NameBackground;
    public Sprite DialogBackground;
    public Sprite LeftBright,RightBright;
    public Sprite LeftDark,RightDark;
    public Sprite LeftBackground,RightBackground;
    public Sprite EndCG;

    [Header("數值")]
    public int TextIndex;
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
        TextName.SetActive(true);
        TextLabel.SetActive(true);
        LeftPicture.SetActive(true);
        RightPicture.SetActive(true);
        Name.SetActive(true);
        Dialog.SetActive(true);
        CG.SetActive(false);
        Name.GetComponent<Image>().sprite = NameBackground;
        Dialog.GetComponent<Image>().sprite = DialogBackground;
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Space) && TextIndex == TextList.Count)
        {
            TextFinished = true;
            TextName.SetActive(false);
            TextLabel.SetActive(false);
            LeftPicture.SetActive(false);
            RightPicture.SetActive(false);
            Name.SetActive(false);
            Dialog.SetActive(false);
            CG.SetActive(true);
            TextIndex = 0;
            CG.GetComponent<Image>().sprite = EndCG;
            return;
        }*/
        StartCoroutine(End());

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
        TextIndex = 0;

        var LineData = file.text.Split('\n');
        
        foreach (var Line in LineData)
        {
            TextList.Add(Line);
        }

    }

    IEnumerator SetTextUI()
    {
        TextFinished = false;
        TextLabel.GetComponent<Text>().text = "";

        if (TextList[TextIndex][0] == 'P')
        {
            LeftPicture.GetComponent<Image>().sprite = LeftBright;
            RightPicture.GetComponent<Image>().sprite = RightDark;
            TextName.GetComponent<Text>().text = "Pekora";
            TextIndex++;
        }

        if (TextList[TextIndex][0] == 'A')
        {
            LeftPicture.GetComponent<Image>().sprite = LeftDark;
            RightPicture.GetComponent<Image>().sprite = RightBright;
            TextName.GetComponent<Text>().text = "Aqua";
            TextIndex++;
        }

        int Letter = 0;
        while (!QuickShow && Letter < TextList[TextIndex].Length -1)
        {
            TextLabel.GetComponent<Text>().text += TextList[TextIndex][Letter];
            Letter++;
            yield return new WaitForSeconds(TextSpeed);
        }

        TextLabel.GetComponent<Text>().text = TextList[TextIndex];
        QuickShow = false;
        TextFinished = true;
        TextIndex++;
    }

    IEnumerator End()
    {
        if(Input.GetKeyDown(KeyCode.Space) && TextIndex == TextList.Count)
        {
            TextFinished = true;
            TextName.SetActive(false);
            TextLabel.SetActive(false);
            LeftPicture.SetActive(false);
            RightPicture.SetActive(false);
            Name.SetActive(false);
            Dialog.SetActive(false);
            CG.SetActive(true);
            TextIndex = 0;
            CG.GetComponent<Image>().sprite = EndCG;

            yield return new WaitForSeconds(1);

            SceneManager.LoadScene("Start");
        }
    }
}