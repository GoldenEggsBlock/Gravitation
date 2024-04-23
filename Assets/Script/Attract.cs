using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract // 吸引关系
{
    public Entity a;
    public Entity b;
    public bool isCollide; // 是否接触
    private Vector3 posLocal; // b相对于a的位置
    private Vector3 lastPosLocal; // 上一帧b相对于a的位置
    private float gravityRatio = 1f; // 重力比例
    private Vector3 direction,force;
    private float stayTime; // 静止时间
    private float stayTimeMax = .5f;
    public bool AttractUpdate() // 每帧更新, true表示应删除此吸引关系
    {
        if (a.rb && b.rb) // a,b有可能和c合并、出界导致刚体被销毁
        {
            AddForce();
            return StateJudge();
        }
        return true;
    }

    private bool StateJudge() // 状态判断,true表示合并
    {
        posLocal = b.transform.InverseTransformPoint(a.transform.position);
        if (isCollide && (posLocal - lastPosLocal).magnitude<.5f) // 两者接触且近乎相对静止
        {
            stayTime += Time.deltaTime;
            if (stayTime > stayTimeMax) // 保持数秒后合并
            {
                a.Merge(b);
                return true;
            }
        }
        else
        {
            stayTime = 0;
        }
        lastPosLocal = posLocal;
        return false;
    }
    
    private void AddForce() // 相互吸引
    {
        direction = (a.transform.position - b.transform.position).normalized;
        force = direction * (a.mass * b.mass * gravityRatio);
        a.rb.AddForce(-force);
        b.rb.AddForce(force);
    }
}
