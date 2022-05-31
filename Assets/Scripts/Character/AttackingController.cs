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
    public bool isExplosion;
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
        if (isExplosion && timer > 0.1) 
            return;
        if (col.gameObject.tag != "Player" && col.gameObject.tag != "Enemy")
            return;
        if (target != "" && col.gameObject.tag != target)
            return;
        //print(col.gameObject.name);
        if (damage > 0)
            if (isExplosion)
                col.gameObject.GetComponent<CreatureController>().getAttacked(damage, from, transform.position.x, blockTime, "Explode");
            else
                col.gameObject.GetComponent<CreatureController>().getAttacked(damage, from, transform.localScale.x, blockTime);
        if (damage < 0)
            col.gameObject.GetComponent<CreatureController>().getAttacked(0, from, transform.localScale.x, blockTime, "Donchan");
        if (destoryByFall)
            Destroy(gameObject);
    }
}
