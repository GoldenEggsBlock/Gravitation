using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Player player;

    private void Update()
    {
        Follow();
    }
    
    private void Follow()
    {
        if(!player) return;
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-10);
    }
}
