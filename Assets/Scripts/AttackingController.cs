using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingController : MonoBehaviour
{
    public string target;
    public int damage;
    public float blockTime;
    public float CD;
    public float destoryTime;
    public bool destoryByFall;
    public string from;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        timer = timer + Time.deltaTime;
        if (timer > destoryTime)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (destoryByFall && col.gameObject.tag == "Ground")
            Destroy(gameObject);
        if (target != "" && col.gameObject.tag != target)
            return;
        print(col.gameObject.name);
        if (damage > 0)
            col.gameObject.GetComponent<CreatureController>().getAttacked(damage, from, blockTime);
        if (damage < 0)
            col.gameObject.GetComponent<CreatureController>().getAttacked(0, from, blockTime, "Donchan");
        if (destoryByFall)
            Destroy(gameObject);
    }
}
