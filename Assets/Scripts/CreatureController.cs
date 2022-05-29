using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CreatureController : MonoBehaviour
{
    public int maxHp;
    public int hp;
    public int atk;

    public float speed;
    public float jumpspeed;
    public float bound;
    [SerializeField] protected bool isGround;
    [SerializeField] protected bool isWalk;
    [SerializeField] protected bool isJump;
    [SerializeField] protected bool isHurt;
    [SerializeField] protected bool isDonchan;
    [SerializeField] protected bool isAttacking;
    [SerializeField] protected float blockedTimer;

    protected Rigidbody2D rig;
    protected BoxCollider2D bc;
    protected Animator animator;

    public void setAttackingTrue()
    {
        isAttacking = true;
    }

    public void setAttackingFalse()
    {
        isAttacking = false;
    }

    public void getAttacked(int damage, float block=0.5f, string type="Hurt")
    {
        if (damage > hp * 10)
        {
            print("CG attack");
        }
        if (type == "Hurt")
            isHurt = true;
        if (type == "Donchan")
            isDonchan = true;
        hp -= damage;
        blockedTimer = Time.time + block;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected bool isBlocked()
    {
        return blockedTimer > Time.time;
    }

    protected void init()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        isGround = false;
        isWalk = false;
        isJump = false;
        isHurt = false;
        isDonchan = false;
        isAttacking = false;
        hp = maxHp;
        blockedTimer = 0;
    }

    protected void checkGround()
    {
        float scalex = transform.localScale.x;
        float scaley = transform.localScale.y;
        float sizex = bc.size.x;
        float sizey = bc.size.y;
        float posx = transform.position.x + bc.offset.x * scalex;
        float posy = transform.position.y + bc.offset.y * scaley;

        float startx = posx - sizex / 2 * scalex;
        float starty = posy - sizey / 2 * scaley;

        int N = 3;

        for (int i = 0; i < N; i++)
        {
            isGround = Physics2D.Linecast(transform.position,
                            new Vector3(startx + (sizex / (N - 1) * i) * scalex, starty - bound * scaley, 1),
                            1 << LayerMask.NameToLayer("Ground"));
            Debug.DrawLine(transform.position,
                            new Vector3(startx + (sizex / (N - 1) * i) * scalex, starty - bound * scaley, 1));
            if (isGround)
                break;
        }
    }

    protected bool isFrontGround()
    {
        float scalex = transform.localScale.x;
        float scaley = transform.localScale.y;
        float sizex = bc.size.x;
        float sizey = bc.size.y;
        float posx = transform.position.x + bc.offset.x * scalex;
        float posy = transform.position.y + bc.offset.y * scaley;

        float startx = posx + sizex / 2 * scalex;
        float starty = posy - sizey / 2 * scaley;

        int N = 5;

        bool re = false;
        for (int i = 0; i < N; i++)
        {
            re = Physics2D.Linecast(transform.position,
                            new Vector3(startx + bound * scalex, starty + (sizey / (N - 1) * i) * scaley, 1),
                            1 << LayerMask.NameToLayer("Ground"));
            Debug.DrawLine(transform.position,
                            new Vector3(startx + bound * scalex, starty + (sizey / (N - 1) * i) * scaley, 1));
            if (re)
                return re;
        }
        return re;
    }

    protected virtual void movingControll(float horizontal)
    {
        if (horizontal != 0)
        {
            float scaleX = Math.Abs(transform.localScale.x);
            if (horizontal < 0)
                transform.localScale = new Vector3(scaleX * -1.0f, transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(scaleX * 1.0f, transform.localScale.y, transform.localScale.z);
            if (!isFrontGround())
                rig.velocity = new Vector2(horizontal * speed, rig.velocity.y);
            else
                rig.velocity = new Vector2(0, rig.velocity.y);

        }
        else
            rig.velocity = new Vector2(0, rig.velocity.y);

        isWalk = horizontal != 0;
    }

    protected virtual void jumpingControll(float vertical)
    {
        isJump = false;
        if (vertical < 0 || !isGround)
            vertical = 0;
        if (vertical > 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, vertical * jumpspeed);
            isJump = true;
        }
    }

    protected virtual void animationControll()
    {
        animator.SetBool("Stay", (isGround && !isWalk && !isJump) || isJump || !isGround);
        animator.SetBool("Walk", isGround && isWalk && !isJump);
        animator.SetBool("Hurt", isHurt);
        isHurt = false;
    }
}
