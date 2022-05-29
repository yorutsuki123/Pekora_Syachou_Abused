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
    public GameObject attack2Prefab;
    public GameObject attack3Prefab;

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
        movingControll(isAttacking ? 0 : Input.GetAxisRaw("Horizontal"));
        jumpingControll(isAttacking ? 0 : Input.GetAxisRaw("Vertical"));
        animationControll();

    }

    protected override void animationControll()
    {
        animator.SetBool("Stay", isGround && !isWalk && !isJump);
        animator.SetBool("Walk", isGround && isWalk && !isJump);
        animator.SetBool("Jump", isJump);
        animator.SetBool("Sky", !isGround);
        animator.SetBool("Attack1", !isAttacking && Input.GetAxisRaw("Fire1") != 0);
        animator.SetBool("Attack2", !isAttacking && Input.GetAxisRaw("Fire2") != 0);
        animator.SetBool("Attack3", !isAttacking && Input.GetAxisRaw("Fire3") != 0);
        animator.SetBool("Hurt", isHurt);
        isHurt = false;
    }

    public void attack1()
    {
        GameObject atkObj = Instantiate(attack1Prefab, transform.position + new Vector3(1.43f * (transform.localScale.x < 0 ? -1 : 1), 0.7f), Quaternion.Euler(new Vector3()));
        atkObj.GetComponent<AttackingController>().damage = atk * (int)Math.Pow(2, buff);
    }
    public void attack2()
    {
        GameObject atkObj = Instantiate(attack2Prefab, transform.position + new Vector3(1.19f * (transform.localScale.x < 0 ? -1 : 1), 0.24f), Quaternion.Euler(new Vector3()));
        Rigidbody2D atkRig = atkObj.GetComponent<Rigidbody2D>();
        atkObj.GetComponent<AttackingController>().damage = atk * (int)Math.Pow(2, buff);
        atkObj.transform.localScale = new Vector3(atkObj.transform.localScale.x * (transform.localScale.x < 0 ? -1 : 1), atkObj.transform.localScale.y, atkObj.transform.localScale.z);
        atkRig.velocity = new Vector2(25 * (transform.localScale.x < 0 ? -1 : 1), atkRig.velocity.y);
    }
    public void attack3()
    {
        GameObject atkObj = Instantiate(attack3Prefab, transform.position + new Vector3(1.69f * (transform.localScale.x < 0 ? -1 : 1), 0.672f), Quaternion.Euler(new Vector3()));
        Rigidbody2D atkRig = atkObj.GetComponent<Rigidbody2D>();
        atkObj.GetComponent<AttackingController>().damage = -1;
        atkObj.transform.localScale = new Vector3(atkObj.transform.localScale.x * (transform.localScale.x < 0 ? -1 : 1), atkObj.transform.localScale.y, atkObj.transform.localScale.z);
        atkRig.velocity = new Vector2(15 * (transform.localScale.x < 0 ? -1 : 1), atkRig.velocity.y);
    }
}
