using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItemController : MonoBehaviour
{
    public int buff;
    public float effectTime;

    float destoryTime;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().buff += buff;
        destoryTime = Time.time + effectTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (destoryTime < Time.time)
        {
            player.GetComponent<PlayerController>().buff -= buff;
            Destroy(gameObject);
        }
    }
}
