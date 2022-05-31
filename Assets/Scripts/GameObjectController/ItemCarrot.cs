using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCarrot : ItemPickup
{
    protected override void effect(GameObject obj)
    {
        obj.GetComponent<PlayerController>().bullet++;
    }
}
