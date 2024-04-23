using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCircle : MonoBehaviour
{
    public CircleCollider2D circleCollider2D;
    private void OnTriggerExit2D(Collider2D other)
    {
        var g = other.gameObject;
        if (g.CompareTag("Sun"))
        {
            if (Vector3.Distance(transform.position,g.transform.position)>=circleCollider2D.radius) // 坠日后不删太阳
            {
                Destroy(g);
                Map.sunCount--;
            }
        }
        else
        {
            Destroy(g);
            Map.entityCount--;
        }
    }
}
