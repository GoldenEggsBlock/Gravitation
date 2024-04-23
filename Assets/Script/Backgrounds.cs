using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgrounds : MonoBehaviour
{
    public SpriteRenderer backgroundPre;
    public Transform playerTrans;
    private static float backgroundWidth;
    private static float backgroundHeight;
    private void Start()
    {
        backgroundWidth = backgroundPre.bounds.size.x;
        backgroundHeight = backgroundPre.bounds.size.y;
        transform.position = GetPos(playerTrans.position);
        for (var i = -2; i <= 2; i++)
        {
            for (var j = -2; j <= 2; j++)
            {
                Instantiate(backgroundPre, new Vector3(i * backgroundWidth, j * backgroundHeight, 0), Quaternion.identity,transform);
            }
        }
    }

    private Vector2 GetPos(Vector2 pos0) // 获取规整坐标
    {
        return new Vector2(backgroundWidth * (int)(pos0.x/backgroundWidth), backgroundHeight * (int)(pos0.y/backgroundHeight));
    }
    
    private void Update()
    {
        transform.position = GetPos(playerTrans.position);
    }
}
