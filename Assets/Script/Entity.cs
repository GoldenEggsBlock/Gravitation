using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour
{
    public readonly List<Attract> attracts = new List<Attract>(); // 吸引关系
    public readonly List<Attract> attractsDel = new List<Attract>(); // 待删除的吸引关系
    public Rigidbody2D rb;
    public float p;
    public CircleCollider2D gravityCircle;
    private int i;
    public bool isMerged; // 是否被合并
    public float mass; // 即便刚体被销毁也能记录质量
    public void Awake()
    {
        var ratio = Random.Range(0.7f, 2f);
        mass = rb.mass;
        p = mass / transform.localScale.x;
        transform.localScale *= ratio; // localScale.x视为r（实则2*r），mass视为s，s=p*r^2
        mass *= ratio * ratio;
        rb.mass = mass;
    }

    public int Find(Entity b)
    {
        return attracts.FindIndex(attract => attract.b == b);
    }

    private void UpdateAttract()
    {
        foreach (var attract in attracts)
        {
            if (attract.AttractUpdate()) // 几乎相对静止则删除
            {
                attractsDel.Add(attract);
            }
        }
        if(attractsDel.Count == 0) return;
        foreach (var attractDel in attractsDel)
        {
            attracts.Remove(attractDel); // 删除吸引关系
        }
        attractsDel.Clear();
    }
    
    public void FixedUpdate()
    {
        if (isMerged) return;
        UpdateAttract();
    }
    
    public void Merge(Entity b) // ab合并
    {
        if(CompareTag("Sun") || b.CompareTag("Sun")) return;
        
            if (CompareTag("Player")) // b合并到a
            {
                Merge0(this,b);
                b.tag = "Player";
            }
            else if(b.CompareTag("Player")) // 如果b是玩家，则强制a合并到b
            {
                Merge0(b,this);
                tag = "Player";
            }
            else
            {
                Merge0(this,b);
            }
        
    }

    private void Merge0(Entity a,Entity b)
    {
        b.transform.parent = a.transform;
        a.mass += b.mass;
        a.rb.mass = a.mass;
        a.gravityCircle.radius = Mathf.Min(4 + (a.mass - 1) * .1f, 20);
        a.rb.angularDrag = 1 + (a.mass - 1) * .1f;
        b.isMerged = true;
        b.enabled = false;
        if (b.CompareTag("Ice"))
        {
            b.rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {

        }
        Destroy(b.rb);
        Destroy(b.gravityCircle.gameObject); // 清除引力圈
        Map.entityCount--;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        SetCollideState(other,true);
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        SetCollideState(other,false);
    }

    private void SetCollideState(Collision2D other,bool f) // 设置吸引关系的接触状态
    {
        var entityOther=other.gameObject.GetComponent<Entity>();
        var i = Find(entityOther);
        if (i>=0)
        {
            attracts[i].isCollide = f;
        }
    }
}
