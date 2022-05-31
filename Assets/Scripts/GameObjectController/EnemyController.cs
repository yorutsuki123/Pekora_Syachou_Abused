using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EnemyController : CreatureController
{
    public float stayDistX;
    public float stayMinY;
    public float stayMaxY;
    public float trackDistX;
    public float trackDistY;
    public GameObject attackPrefab;

    [SerializeField] protected bool isDonchan;
    [SerializeField] protected bool isContiDonchan;
    [SerializeField] protected bool isCanAttack;
    [SerializeField] protected bool isAttack;
    [SerializeField] protected float attackTimer;

    protected GameObject player;
    protected Vector3 target;

    // Start is called before the first frame update
    protected void Start()
    {
        init();
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    protected void FixedUpdate()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");
        if (player != null)
            target = player.transform.position;
        checkGround();
        attackControll();
        float m = getMovement();
        movingControll(m);
        jumpingControll((m != 0 && isFrontGround()) ? 1 : 0);
        animationControll();
    }

    protected override void init()
    {
        base.init();
        isDonchan = false;
        isContiDonchan = false;
        isCanAttack = false;
        isAttack = false;
        attackTimer = 0;
    }

    protected virtual void attackControll()
    {
        isAttack = (isCanAttack && !isAttacking && !isBlocked() && attackTimer <= Time.time);
        if (isAttack)
        {
            if (transform.position.x > target.x)
                transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * -1.0f, transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * 1.0f, transform.localScale.y, transform.localScale.z);
        }
    }

    protected virtual float getMovement()
    {
        isCanAttack = false;
        if (target == null)
            return 0;
        float selfX = transform.position.x;
        float selfY = transform.position.y;
        if (Math.Abs(selfX - target.x) > trackDistX || Math.Abs(selfY - target.y) > trackDistY)
            return 0;
        if (Math.Abs(selfX - target.x) < stayDistX)
        {
            isCanAttack = (stayMinY < (target.y - selfY) && (target.y - selfY) < stayMaxY);
            return 0;
        }
        return (selfX - target.x) < 0 ? 1 : -1;
    }

    public override void getAttacked(int damage, string from, float direction, float block = 0.5f, string type = "Hurt")
    {
        base.getAttacked(damage, from, direction, block, type);
        if (type == "Donchan")
        {
            isDonchan = true;
            isContiDonchan = true;
        }
    }

    protected override void animationControll()
    {
        if (isBlocked() == false)
            isContiDonchan = false;
        animator.SetBool("Stay", !isContiDonchan && ((isGround && !isWalk && !isJump) || isJump || !isGround));
        animator.SetBool("Walk", !isContiDonchan && isGround && isWalk && !isJump);
        animator.SetBool("Hurt", isHurt);
        animator.SetBool("Donchan", isDonchan);
        animator.SetBool("Donchaning", isContiDonchan);
        animator.SetBool("Attack", isAttack);
        isHurt = isDonchan = isAttack = false;
    }

    private void OnDestroy()
    {
        GameObject.FindWithTag("MainCamera").GetComponent<GameRule>().randomDropItem(transform.position);
    }

    protected abstract void attack();
}
