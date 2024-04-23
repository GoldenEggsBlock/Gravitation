using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCircle : MonoBehaviour
{
    public float waterMass;
    public CircleCollider2D waterCircle;
    public SpriteRenderer sprite;
    public void UpdateWater(float d)
    {
        waterMass += d;
        if (waterMass < 0)
        {
            waterCircle.radius = 0;
            waterMass = 0;
            sprite.transform.localScale = Vector3.zero;
        }
        else
        {
            waterCircle.radius = Mathf.Sqrt(waterMass); // r
            sprite.transform.localScale = 2 * waterCircle.radius * Vector3.one; // 2r
        }
    }
}
