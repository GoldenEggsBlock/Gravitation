using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : Entity
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        var g=other.gameObject;
        if(g.CompareTag("Sun")) return;
        if (g.CompareTag("Player"))
        {
            Map.map.KillPlayer();
            attracts.Clear();
            attractsDel.Clear();
            return;
        }
        if (g)
        {
            Destroy(g);
            Map.entityCount--;
        }
    }
}
