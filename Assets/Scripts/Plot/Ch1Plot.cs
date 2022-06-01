using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ch1Plot : MonoBehaviour
{
    
    [Header("文本文件")]
    public TextAsset TextFile;

    [Header("UI組件")]
    public Text TextName;
    public Text TextLabel;
    public Image LeftPicture,RightPicture;
    public Image Name,Dialog;

    [Header("圖片")]
    public Sprite NameBackground;
    public Sprite DialogBackground;
    public Sprite LeftBright,RightBright;
    public Sprite LeftDark,RightDark;
    public Sprite LeftBackground,RightBackground;

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
        Name.sprite = NameBackground;
        Dialog.sprite = DialogBackground;
        LeftPicture.sprite = LeftBackground;
        RightPicture.sprite = RightBackground;
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && TextIndex == TextList.Count)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("World1");
            TextIndex = 0;
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
        TextLabel.text = "";

        if (TextList[TextIndex][0] == 'P')
        {
            LeftPicture.sprite = LeftBright;
            RightPicture.sprite = RightDark;
            TextName.text = "Pekora";
            TextIndex++;
        }

        if (TextList[TextIndex][0] == 'M')
        {
            LeftPicture.sprite = LeftDark;
            RightPicture.sprite = RightBright;
            TextName.text = "Moona";
            TextIndex++;
        }

        int Letter = 0;
        while (!QuickShow && Letter < TextList[TextIndex].Length -1)
        {
            TextLabel.text += TextList[TextIndex][Letter];
            Letter++;
            yield return new WaitForSeconds(TextSpeed);
        }

        TextLabel.text = TextList[TextIndex];
        QuickShow = false;
        TextFinished = true;
        TextIndex++;
    }
}