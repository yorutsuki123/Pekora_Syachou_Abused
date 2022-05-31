using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    public float boundMin;
    public float boundMax;
    public float vertical;
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
            player = GameObject.FindWithTag("Player");
        else
        {
            Vector3 pos = player.transform.position;
            if (boundMin > pos.x)
                transform.position = new Vector3(boundMin, pos.y + vertical, transform.position.z);
            else if (boundMax < pos.x)
                transform.position = new Vector3(boundMax, pos.y + vertical, transform.position.z);
            else
                transform.position = new Vector3(pos.x, pos.y + vertical, transform.position.z);
        }
    }
}
