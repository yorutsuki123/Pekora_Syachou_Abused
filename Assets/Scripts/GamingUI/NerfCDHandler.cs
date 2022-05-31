using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NerfCDHandler : MonoBehaviour
{
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
        {
            player = GameObject.FindWithTag("Player");
        }
        if (player != null)
        {
            gameObject.GetComponent<Image>().fillAmount = player.GetComponent<PlayerController>().nerf_posion == 0 ? 1 : 0;
        }
    }
}
