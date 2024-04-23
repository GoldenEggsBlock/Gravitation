using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCircle : MonoBehaviour // 引力圈
{
    public Entity entity;
    
    private void OnTriggerEnter2D(Collider2D other) // 把other添加到吸引关系列表
    {
        var entityOther=other.GetComponent<Entity>();
        if (entityOther && !entityOther.CompareTag("Sun") && entityOther.Find(entity)<0) // 对方未建立吸引关系，则自己建立
        {
            var attract = new Attract
            {
                a = entity,
                b = entityOther
            };
            if(entity)
                entity.attracts.Add(attract);
        }
    }

    private void OnTriggerExit2D(Collider2D other) // 把other从吸引关系列表移除
    {
        var entityOther = other.GetComponent<Entity>();
        if(!entity) return;
        var i = entity.Find(entityOther);
        if(i<0) return;
        entity.attractsDel.Add(entity.attracts[i]);
    }
}
