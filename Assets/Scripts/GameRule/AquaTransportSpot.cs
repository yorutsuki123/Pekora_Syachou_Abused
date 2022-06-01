using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaTransportSpot : MonoBehaviour
{
    public GameObject target;
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
        //print("AQUA TOUCH");
        a.gameObject.transform.position = target.transform.position;
    }
}
