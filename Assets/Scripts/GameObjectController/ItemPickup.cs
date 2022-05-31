using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemPickup : MonoBehaviour
{
    public GameObject effector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        print(col.gameObject.tag);
        if (col.gameObject.tag != "Player")
            return;
        effect(col.gameObject);
        Instantiate(effector);
        Destroy(gameObject);
    }

    protected abstract void effect(GameObject obj);
}
