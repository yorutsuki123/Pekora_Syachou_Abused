using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupEffect : MonoBehaviour
{

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = GameObject.FindWithTag("Player").transform.position;
        transform.position = new Vector3(pos.x, pos.y + 1, pos.z);
        timer = Time.time + 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        if (Time.time > timer)
            Destroy(gameObject);
    }
}
