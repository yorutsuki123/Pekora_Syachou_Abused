using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : EnemyController
{
    // Start is called before the first frame update
    protected override void attack()
    {
        GameObject atkObj = Instantiate(attackPrefab, transform.position + new Vector3(1.188f * (transform.localScale.x < 0 ? -1 : 1), 0.646f), Quaternion.Euler(new Vector3()));
        Rigidbody2D atkRig = atkObj.GetComponent<Rigidbody2D>();
        attackTimer = Time.time + atkObj.GetComponent<AttackingController>().CD;
        atkObj.transform.localScale = new Vector3(atkObj.transform.localScale.x * (transform.localScale.x < 0 ? -1 : 1), atkObj.transform.localScale.y, atkObj.transform.localScale.z);
        atkRig.velocity = new Vector2(20 * (transform.localScale.x < 0 ? -1 : 1), atkRig.velocity.y);
        atkObj.GetComponent<AttackingController>().from = "Skeleton";
    }
}
