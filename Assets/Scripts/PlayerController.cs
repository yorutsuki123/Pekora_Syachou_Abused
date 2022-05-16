using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpspeed;
    public GameObject[] groundChecker = new GameObject[3];
    [SerializeField] bool isGround;
    [SerializeField] bool isWalk;
    [SerializeField] bool isJump;

    Rigidbody2D rig;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
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
        for (int i = 0; i < 3; i++)
        {
            isGround = Physics2D.Linecast(transform.position,
                            groundChecker[i].transform.position,
                            1 << LayerMask.NameToLayer("Ground"));
            if (isGround)
                break;
        }
    }

    void movingControll()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (isGround)
        {
            if (horizontal != 0)
            {
                float scaleX = Math.Abs(transform.localScale.x);
                if (horizontal < 0)
                    transform.localScale = new Vector3(scaleX * -1.0f, transform.localScale.y, transform.localScale.z);
                else
                    transform.localScale = new Vector3(scaleX * 1.0f, transform.localScale.y, transform.localScale.z);
                rig.velocity = new Vector2(horizontal * speed, rig.velocity.y);
            }
            else
                rig.velocity = new Vector2(0, rig.velocity.y);
        }

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
