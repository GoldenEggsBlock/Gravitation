using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Comet : Entity // 彗星
{
    public Ice icePre;
    private void Start()
    {
        rb.velocity = Random.insideUnitCircle * Random.Range(.5f, 5);
    }
    
    // 碰撞后分裂
    private void OnCollisionEnter2D(Collision2D other)
    {
        for(var i =1; i<= Random.Range(4,7); i++)
        {
            var ice = Instantiate(icePre, transform.position, Quaternion.identity,transform.parent);
            ice.e.rb.velocity = Random.insideUnitCircle * Random.Range(10, 25);
            if (i > 1)
                Map.entityCount++;
        }
        if(gameObject)
            Destroy(gameObject);
    }
}
