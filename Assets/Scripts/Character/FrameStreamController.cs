using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrameStreamController : MonoBehaviour
{
    public float speed;
    public GameObject attacker;
    public float lambda;
    public float atkSpeed;
    public float time;
    float orgZ;
    float timer;
    float desTimer;
    float atkerScaleX;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time + lambda;
        desTimer = Time.time + time;
        orgZ = transform.localScale.z;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
        atkerScaleX = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (desTimer < Time.time)
            Destroy(gameObject);
        if (transform.localScale.z < orgZ)
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Math.Min(transform.localScale.z + Time.deltaTime * speed, orgZ));
        if (timer < Time.time)
        {
            float r = (transform.rotation.eulerAngles.y - 180) / -90;
            GameObject a = Instantiate(
                attacker, 
                new Vector3(transform.position.x + r * Math.Min(transform.localScale.z * 8, atkerScaleX += atkSpeed) / 2, transform.position.y), 
                Quaternion.Euler(new Vector3()));
            a.transform.localScale = new Vector3(r * Math.Min(transform.localScale.z * 8, atkerScaleX += atkSpeed), a.transform.localScale.y, a.transform.localScale.z);
            timer = Time.time + lambda;
        }
    }
}
