using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingController : MonoBehaviour
{
    public string target;
    public int damage;

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
        if (timer > 0.25)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (target != "" && col.gameObject.tag != target)
            return;
        print(col.gameObject.name);
        col.gameObject.GetComponent<CreatureController>().getAttacked(damage);
    }
}
