using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : CreatureController
{
    public float stayDistX;
    public float trackDistX;
    public float trackDistY;

    protected GameObject player;
    protected Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");
        if (player != null)
            target = player.transform.position;
        checkGround();
        movingControll(getMovement());
        jumpingControll(0);
        animationControll();
    }

    protected virtual float getMovement()
    {
        if (target == null)
            return 0;
        float selfX = transform.position.x;
        float selfY = transform.position.y;
        if (Math.Abs(selfX - target.x) > trackDistX || Math.Abs(selfY - target.y) > trackDistY)
            return 0;
        if (Math.Abs(selfX - target.x) < stayDistX)
            return 0;
        return (selfX - target.x) < 0 ? 1 : -1;
    }

}
