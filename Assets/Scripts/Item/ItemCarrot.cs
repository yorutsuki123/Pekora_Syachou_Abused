using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCarrot : ItemPickup
{
    public int n;
    protected override void effect(GameObject obj)
    {
        obj.GetComponent<PlayerController>().bullet += n;
    }
}
