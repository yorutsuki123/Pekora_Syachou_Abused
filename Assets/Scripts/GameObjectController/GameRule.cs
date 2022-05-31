using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRule : MonoBehaviour
{

    public GameObject[] dropItem = new GameObject[4];
    public float[] dropProb = new float[4];


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void randomDropItem(Vector3 pos)
    {
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
