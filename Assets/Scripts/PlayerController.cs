using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : CreatureController
{
    public int buff;
    public int bullet;
    public bool ableDonchan;

    public GameObject attack1Prefab;

    // Start is called before the first frame update
    void Start()
    {
        init();
        buff = 0;
        ableDonchan = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        checkGround();
        movingControll(isAttacking || isBlocked() ? 0 : Input.GetAxisRaw("Horizontal"));
        jumpingControll(isAttacking || isBlocked() ? 0 : Input.GetAxisRaw("Vertical"));
        animationControll();

    }

    protected override void animationControll()
    {
        animator.SetBool("Stay", isGround && !isWalk && !isJump);
        animator.SetBool("Walk", isGround && isWalk && !isJump);
        animator.SetBool("Jump", isJump);
        animator.SetBool("Sky", !isGround);
        animator.SetBool("Attack1", !isAttacking && Input.GetAxisRaw("Fire1") != 0);
        animator.SetBool("Hurt", isHurt);
        isHurt = false;
    }

    public void attack1()
    {
        GameObject atkObj = Instantiate(attack1Prefab, transform.position + new Vector3(1.43f * (transform.localScale.x < 0 ? -1 : 1), 0.7f), Quaternion.Euler(new Vector3()));
        atkObj.GetComponent<AttackingController>().damage = atk * (int)Math.Pow(2, buff);
    }
    
}
