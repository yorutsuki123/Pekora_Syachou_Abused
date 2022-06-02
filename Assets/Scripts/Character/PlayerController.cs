using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : CreatureController
{
    public int buff;
    public int bullet;
    public int buff_posion;
    public int nerf_posion;
    public float donchanTimer;
    public float donchanCD;

    public GameObject attack1Prefab;
    public GameObject attack2Prefab;
    public GameObject attack3Prefab;
    public GameObject item1Prefab;
    public GameObject item2Prefab;
    public GameRule gameRule;

    bool isItem1InUse;
    bool isItem2InUse;

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
        float h = 0, v = 0;
        checkGround();
        if (hp > 0)
        {
            try
            {
                if (gameRule == null)
                    gameRule = GameObject.FindWithTag("MainCamera").GetComponent<GameRule>();
                else if (gameRule.isEnd)
                    return;
            }
            catch (System.Exception)
            {

            }
            if (Input.GetAxisRaw("Item1") > 0)
            {
                if (!isItem1InUse)
                {
                    if (buff_posion > 0)
                    {
                        Instantiate(item1Prefab, new Vector3(), Quaternion.Euler(new Vector3()));
                        buff_posion--;
                    }
                }
                isItem1InUse = true;
            }
            else
            {
                isItem1InUse = false;
            }
            if (Input.GetAxisRaw("Item2") > 0)
            {
                if (!isItem2InUse)
                {
                    if (nerf_posion > 0)
                    {
                        Instantiate(item2Prefab, new Vector3(), Quaternion.Euler(new Vector3()));
                        nerf_posion--;
                    }
                }
                isItem2InUse = true;
            }
            else
            {
                isItem2InUse = false;
            }
            h = isAttacking ? 0 : Input.GetAxisRaw("Horizontal");
            v = isAttacking ? 0 : Input.GetAxisRaw("Vertical");
        }
        movingControll(h);
        jumpingControll(v);
        animationControll();

    }

    void getStatus()
    {
        gameRule = GameObject.FindWithTag("MainCamera").GetComponent<GameRule>();
        buff_posion = gameRule.ps.buff;
        nerf_posion = gameRule.ps.nerf;
        bullet = gameRule.ps.bullet;
        hp = gameRule.ps.hp;
    }

    protected override void init()
    {
        base.init();
        getStatus();
        buff = 0;
        donchanTimer = 0;
    }

    public override void getAttacked(int damage, string from, float direction, float block = 0.5f, string type = "Hurt")
    {
        if (hp > 0 && (int)(damage * Math.Pow(2, -1 * buff)) >= hp * 10)
        {
            print("CG attack from " + from);
            gameRule.PekoCG = true;
        }
        base.getAttacked((int)(damage * Math.Pow(2, -1 * buff)), from, direction, block, type);
    }

    protected override void animationControll()
    {
        animator.SetBool("Stay", isGround && !isWalk && !isJump);
        animator.SetBool("Walk", isGround && isWalk && !isJump);
        animator.SetBool("Jump", isJump);
        animator.SetBool("Sky", !isGround);
        animator.SetBool("Attack1", !isAttacking && Input.GetAxisRaw("Fire1") != 0);
        animator.SetBool("Attack2", !isAttacking && bullet > 0 && Input.GetAxisRaw("Fire2") != 0);
        animator.SetBool("Attack3", !isAttacking && nowDonchanCD() == 0 && Input.GetAxisRaw("Fire3") != 0);
        animator.SetBool("Hurt", isHurt);
        isHurt = false;
    }

    public void attack1()
    {
        GameObject atkObj = Instantiate(attack1Prefab, transform.position + new Vector3(1.43f * (transform.localScale.x < 0 ? -1 : 1), 0.7f), Quaternion.Euler(new Vector3()));
        atkObj.transform.localScale = new Vector3(atkObj.transform.localScale.x * (transform.localScale.x < 0 ? -1 : 1), atkObj.transform.localScale.y, atkObj.transform.localScale.z);
        atkObj.GetComponent<AttackingController>().damage = (int)Math.Ceiling(atkObj.GetComponent<AttackingController>().damage * Math.Pow(2, buff));
        atkObj.GetComponent<AttackingController>().from = "Player";
    }

    public void attack2()
    {
        GameObject atkObj = Instantiate(attack2Prefab, transform.position + new Vector3(1.19f * (transform.localScale.x < 0 ? -1 : 1), 0.24f), Quaternion.Euler(new Vector3()));
        Rigidbody2D atkRig = atkObj.GetComponent<Rigidbody2D>();
        atkObj.transform.localScale = new Vector3(atkObj.transform.localScale.x * (transform.localScale.x < 0 ? -1 : 1), atkObj.transform.localScale.y, atkObj.transform.localScale.z);
        atkRig.velocity = new Vector2(25 * (transform.localScale.x < 0 ? -1 : 1), atkRig.velocity.y);
        atkObj.GetComponent<AttackingController>().damage = (int)Math.Ceiling(atkObj.GetComponent<AttackingController>().damage * Math.Pow(2, buff));
        atkObj.GetComponent<AttackingController>().from = "Player";
        bulletMinus();
    }

    public void attack3()
    {
        GameObject atkObj = Instantiate(attack3Prefab, transform.position + new Vector3(1.69f * (transform.localScale.x < 0 ? -1 : 1), 0.672f), Quaternion.Euler(new Vector3()));
        Rigidbody2D atkRig = atkObj.GetComponent<Rigidbody2D>();
        atkObj.transform.localScale = new Vector3(atkObj.transform.localScale.x * (transform.localScale.x < 0 ? -1 : 1), atkObj.transform.localScale.y, atkObj.transform.localScale.z);
        atkRig.velocity = new Vector2(15 * (transform.localScale.x < 0 ? -1 : 1), atkRig.velocity.y);
        atkObj.GetComponent<AttackingController>().damage = -1;
        atkObj.GetComponent<AttackingController>().from = "Player";
        unableDonchan();
    }

    public void bulletMinus()
    {
        bullet--;
    }

    public void unableDonchan()
    {
        donchanTimer = Time.time + donchanCD;
    }

    public float nowDonchanCD()
    {
        return Math.Max(donchanTimer - Time.time, 0);
    }

    protected override void whenDie()
    {
        print("PEKO DEAD");
        if (gameRule.world == 3 && gameRule.PekoCG)
            gameRule.pekoGameOver();
        else
            gameRule.gameOver();
    }
}
