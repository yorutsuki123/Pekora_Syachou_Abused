using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

public class GameRule : MonoBehaviour
{
    public int world;
    public GameObject[] dropItem = new GameObject[4];
    public float[] dropProb = new float[4];
    public bool EndPointSpot;
    public GameObject PlayerPrefab;
    public GameObject StartSpot;
    public GameObject EndSpot;
    public bool isEnd;
    public GameObject player;
    public PlayerStatus ps;

    public GameObject gameOverUI;
    public GameObject youwinUI;
    public GameObject pekoGameOverUI;
    public GameObject aquaGameOverUI;
    public bool PekoCG;
    public bool AquaCG;


    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
        youwinUI.SetActive(false);
        pekoGameOverUI.SetActive(false);
        aquaGameOverUI.SetActive(false);
        ps = new PlayerStatus();
        ps.ReadFromFile();
        if (ps.last_world == world + 1)
        {
            EndPointSpot = true;
        }
        if (EndPointSpot)
        {
            player = Instantiate(PlayerPrefab, EndSpot.transform.position, Quaternion.Euler(new Vector3()));
        }
        else
        {
            player = Instantiate(PlayerPrefab, StartSpot.transform.position, Quaternion.Euler(new Vector3()));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void randomDropItem(Vector3 pos)
    {
        if (isEnd) return;
        float result = Random.Range(0.0f, 1.0f);
        float s = 0;
        for (int i = 0; i < 4; i++)
        {
            s += dropProb[i];
            if (result < s)
            {
                Instantiate(dropItem[i], pos, Quaternion.Euler(new Vector3()));
                return;
            }
        }
    }

    public void saveStatus()
    {
        ps.hp = player.GetComponent<PlayerController>().hp;
        ps.bullet = player.GetComponent<PlayerController>().bullet;
        ps.buff = player.GetComponent<PlayerController>().buff_posion;
        ps.nerf = player.GetComponent<PlayerController>().nerf_posion;
        ps.last_world = world;
        ps.WriteToFile();
    }

    public void gameOver()
    {
        if (world == 3)
            pekoGameOverUI.SetActive(false);
        isEnd = true;
        gameOverUI.SetActive(true);
        //if (world != 3)
            //Time.timeScale = 0;
    }

    public void pekoGameOver()
    {
        isEnd = true;
        pekoGameOverUI.SetActive(true);
    }

    public void pekoraWin()
    {
        isEnd = true;
        youwinUI.SetActive(true);
    }

    public void aquaGameOver()
    {
        if (AquaCG)
        {
            aquaGameOverUI.SetActive(true);
            youwinUI.SetActive(false);
        }
        else
            toEnd();
    }

    public void backtotitle()
    {
        print("back to title");
        SceneManager.LoadScene("Start");
    }

    public void toEnd()
    {
        SceneManager.LoadScene("ChapterEnd");
    }

}

public class PlayerStatus
{
    const string path = "SaveData";
    const string fileName = @"SaveData\save.dat";

    public int hp;
    public int bullet;
    public int buff;
    public int nerf;
    public int last_world;

    public PlayerStatus()
    {
        init();
    }


    public PlayerStatus(int hp, int bullet, int buff, int nerf, int last_world)
    {
        this.hp = hp;
        this.bullet = bullet;
        this.buff = buff;
        this.nerf = nerf;
        this.last_world = last_world;

    }
    void init()
    {
        hp = 20;
        bullet = 20;
        buff = 0;
        nerf = 0;
        last_world = 0;
    }

    public static void createEmpty()
    {
        PlayerStatus s = new PlayerStatus();
        s.WriteToFile();
    }

    public void WriteToFile()
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        using (var stream = File.Open(fileName, FileMode.Create))
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
            {
                writer.Write(hp);
                writer.Write(bullet);
                writer.Write(buff);
                writer.Write(nerf);
                writer.Write(last_world);
            }
        }
    }

    public void ReadFromFile()
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        if (File.Exists(fileName))
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    hp = reader.ReadInt32();
                    bullet = reader.ReadInt32();
                    buff = reader.ReadInt32();
                    nerf = reader.ReadInt32();
                    last_world = reader.ReadInt32();
                }
            }
            if (hp == 0)
            {
                init();
            }

        }
        else
        {
            init();
            WriteToFile();
        }
    }
}