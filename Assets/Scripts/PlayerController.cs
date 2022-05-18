using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpspeed;
    public float bound;
    [SerializeField] bool isGround;
    [SerializeField] bool isWalk;
    [SerializeField] bool isJump;

    Rigidbody2D rig;
    BoxCollider2D bc;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        isGround = false;
        isWalk = false;
        isJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        checkGround();
        movingControll();
        jumpingControll();
        animationControll();
    }

    void checkGround()
    {
        float scalex = transform.localScale.x;
        float scaley = transform.localScale.y;
        float sizex = bc.size.x;
        float sizey = bc.size.y;
        float posx = transform.position.x + bc.offset.x * scalex;
        float posy = transform.position.y + bc.offset.y * scaley;

        float startx = posx - sizex / 2 * scalex;
        float starty = posy - sizey / 2 * scaley;
        for (int i = 0; i < 3; i++)
        {
            isGround = Physics2D.Linecast(transform.position,
                            new Vector3(startx + (sizex / 2 * i) * scalex, starty - bound * scaley, 1),
                            1 << LayerMask.NameToLayer("Ground"));
            Debug.DrawLine(transform.position,
                            new Vector3(startx + (sizex / 2 * i) * scalex, starty - bound * scaley, 1));
            if (isGround)
                break;
        }
    }

    bool isFrontGround()
    {
        float scalex = transform.localScale.x;
        float scaley = transform.localScale.y;
        float sizex = bc.size.x;
        float sizey = bc.size.y;
        float posx = transform.position.x + bc.offset.x * scalex;
        float posy = transform.position.y + bc.offset.y * scaley;

        float startx = posx + sizex / 2 * scalex;
        float starty = posy - sizey / 2 * scaley;
        bool re = false;
        for (int i = 0; i < 4; i++)
        {
            re = Physics2D.Linecast(transform.position,
                            new Vector3(startx + bound * scalex, starty + (sizey / 3 * i) * scaley, 1),
                            1 << LayerMask.NameToLayer("Ground"));
            Debug.DrawLine(transform.position,
                            new Vector3(startx + bound * scalex, starty + (sizey / 3 * i) * scaley, 1));
            if (re)
                return re;
        }
        return re;
    }

    void movingControll()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

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

    void jumpingControll()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        isJump = false;
        if (vertical < 0 || !isGround)
            vertical = 0;
        if (vertical > 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, vertical * jumpspeed);
            isJump = true;
        }
    }

    void animationControll()
    {
        animator.SetBool("Stay", isGround && !isWalk && !isJump);
        animator.SetBool("Walk", isGround && isWalk && !isJump);
        animator.SetBool("Jump", isJump);
        animator.SetBool("Sky", !isGround);
    }
}
