using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRule : MonoBehaviour
{

    public GameObject[] dropItem = new GameObject[4];
    public float[] dropProb = new float[4];
    public bool EndPointSpot;
    public GameObject Player;
    public GameObject StartSpot;
    public GameObject EndSpot;
    public bool isEnd;

    // Start is called before the first frame update
    void Start()
    {
        if (EndPointSpot)
        {
            Instantiate(Player, EndSpot.transform.position, Quaternion.Euler(new Vector3()));
        }
        else
        {
            Instantiate(Player, StartSpot.transform.position, Quaternion.Euler(new Vector3()));
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

}
