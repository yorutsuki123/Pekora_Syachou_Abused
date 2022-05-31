using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreeperController : EnemyController
{
    public float contiExplodeDist;
    public bool isExploding;
    public bool isReadyExploding;

    protected override void init()
    {
        base.init();
        isExploding = false;
        isReadyExploding = false;
    }

    protected override float getMovement()
    {
        isCanAttack = false;
        if (target == null)
            return 0;
        float selfX = transform.position.x;
        float selfY = transform.position.y;
        if (isReadyExploding || isExploding && Math.Abs(selfX - target.x) < contiExplodeDist)
        {
            return 0;
        }
        isExploding = false;
        if (Math.Abs(selfX - target.x) > trackDistX || Math.Abs(selfY - target.y) > trackDistY)
        {
            return 0;
        }
        if (Math.Abs(selfX - target.x) < stayDistX)
        {
            isCanAttack = (stayMinY < (target.y - selfY) && (target.y - selfY) < stayMaxY);
            return 0;
        }
        return (selfX - target.x) < 0 ? 1 : -1;
    }

    void readyAttack()
    {
        isReadyExploding = true;
    }

    protected override void attack()
    {
        GameObject atkObj = Instantiate(attackPrefab, transform.position, Quaternion.Euler(new Vector3()));
        Destroy(gameObject);
    }

    protected override void animationControll()
    {
        if (isBlocked() == false)
            isContiDonchan = false;
        if (isAttack)
            isExploding = true;
        if (isExploding)
            destoryWhenDie();
        animator.SetBool("Stay", !isContiDonchan && !isExploding && ((isGround && !isWalk && !isJump) || isJump || !isGround));
        animator.SetBool("Walk", !isContiDonchan && !isExploding && isGround && isWalk && !isJump);
        animator.SetBool("Hurt", !isExploding && isHurt);
        animator.SetBool("Donchan", !isExploding && isDonchan);
        animator.SetBool("Donchaning", !isExploding && isContiDonchan);
        animator.SetBool("Attack", isAttack);
        isHurt = isDonchan = isAttack = false;
    }
}
