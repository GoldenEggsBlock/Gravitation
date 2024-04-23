using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : Entity
{
    public static float viewMin = 4;
    public static float viewMax = 25;
    private float HorInput;
    private float verInput;
    private float scrollInput;
    private string Hor = "Horizontal";
    private string Ver = "Vertical";
    private string MS = "Mouse ScrollWheel";
    private float speed = 2;
    public CircleCollider2D activeCircle;
    public Camera cameraMain;
    public WaterCircle waterCircle;
    private void Update()
    {
        KeyInput();
    }
    
    private new void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }
    
    private void Move()
    {
        rb.AddForce(new Vector2(HorInput,verInput) * (speed * mass));
    }
    

    private void KeyInput()
    {
        HorInput = Input.GetAxis(Hor);
        verInput = Input.GetAxis(Ver);
        scrollInput = Input.GetAxis(MS);
        if(scrollInput != 0)
            cameraMain.orthographicSize = Mathf.Clamp(cameraMain.orthographicSize - scrollInput * 4, viewMin, viewMax);
    }
}
