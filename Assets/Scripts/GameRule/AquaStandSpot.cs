using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaStandSpot : MonoBehaviour
{
    public bool left;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        AquaController a = col.GetComponent<AquaController>();
        if (a == null)
            return;
        if (left ^ a.isLeft)
        {
            a.isLeft = left;
            a.isPlaying = false;
        }
    }
}
