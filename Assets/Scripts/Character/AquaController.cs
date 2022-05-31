using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AquaController : EnemyController
{

    public bool isPlaying;
    public bool isLeft;

    int attackNum;
    int counter;

    protected override void FixedUpdate()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");
        if (player != null)
            target = player.transform.position;
        checkGround();
        attackControll();
        animationControll();
    }

    protected override void attackControll()
    {
        if (isPlaying)
        {
            movingControll(isLeft ? -1 : 1);
        }
        else if (!isAttacking || attackTimer > Time.time)
        {
            switch(counter % 6)
            {
                case 0:
                    attackNum = 1;
                    break;
                case 1:
                    attackTimer = Time.time + 2;
                    break;
                case 2:
                    attackNum = 2;
                    break;
                case 3:
                    attackTimer = Time.time + 2;
                    break;
                case 4:
                    attackNum = 3;
                    break;
                case 5:
                    isPlaying = true;
                    break;
            }
            counter++;
        }
    }

    protected override void attack()
    {

    }

    public override void getAttacked(int damage, string from, float direction, float block = 0.5f, string type = "Hurt")
    {
        if (damage > hp * 10)
        {
            print("CG attack from " + from);
        }
        if (type == "Hurt" || type == "Explode")
        {
            isHurt = true;
            hp -= damage;
        }
        if (type == "Donchan")
        {
            isDonchan = true;
            isContiDonchan = true;
        }
        if (blockedTimer < Time.time + block)
            blockedTimer = Time.time + block;
    }

    protected override void animationControll()
    {
        if (isBlocked() == false)
            isContiDonchan = false;
        animator.SetBool("Stay", !isContiDonchan && ((isGround && !isWalk) || !isGround));
        animator.SetBool("Walk", !isContiDonchan && isGround && isWalk);
        animator.SetBool("Hurt", isHurt && !isAttacking);
        animator.SetBool("Donchan", isDonchan);
        animator.SetBool("Donchaning", isContiDonchan);
        animator.SetInteger("Attack", attackNum);
        isHurt = isDonchan = false;
        attackNum = 0;
    }

    protected override void whenDie()
    {
        print("BOSS DEAD");
    }

}
