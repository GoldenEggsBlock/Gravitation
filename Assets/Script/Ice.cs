using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public float warmSpeed; // 融化速度
    public Entity e;
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (warmSpeed <= 0) return; // 融化
        e.mass -= warmSpeed;
        if (e.rb)
        {
            e.rb.mass = e.mass;
        }
        if (e.mass <= 0)
        {
            Destroy(gameObject);
            Map.entityCount--;
            return;
        }
        transform.localScale = Vector3.one * (Mathf.Sqrt(e.mass/e.p));
        if (transform.parent.CompareTag("Player"))
        {
            Map.map.player.waterCircle.UpdateWater(warmSpeed);
        }
    }
}
