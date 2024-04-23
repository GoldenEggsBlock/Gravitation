using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    public static Map map;
    public Stone[] stonePre;
    public Player player;
    public static int entityCount;
    private static int entityCountMax = 200;
    public Sun sunPre;
    public Comet cometPre;
    public static int sunCount;
    private static int sunCountMax = 1;
    public static float rMinCreate; // 最小生成距离
    private void Start()
    {
        map = this;
        InitMap();
        var l = Player.viewMax * 1920 / 1080f;
        rMinCreate = Mathf.Sqrt(Player.viewMax * Player.viewMax + l * l);
    }
    
    public void KillPlayer()
    {
        StartCoroutine(KillPlayer0());
    }
    private IEnumerator KillPlayer0()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        player.attracts.Clear();
        player.attractsDel.Clear();
        player.transform.position += (Vector3)Random.insideUnitCircle.normalized * Random.Range(40,50);
        player.gameObject.SetActive(true);
    }
    private void InitMap()
    {
        for (var i = 0; i < Random.Range(3, 5); i++)
            CreateEntity(stonePre[Random.Range(0, stonePre.Length)],4,10);
    }

    private void FixedUpdate()
    {
        if(!player) return;
        if (Random.value < .1f && entityCount < entityCountMax)
        {
            CreateEntity(stonePre[Random.Range(0, stonePre.Length)], rMinCreate, player.activeCircle.radius - 5);
            entityCount++;
        }
        else if (Random.value < .003f && sunCount < sunCountMax)
        {
            CreateEntity(sunPre, player.activeCircle.radius - 10, player.activeCircle.radius - 5);
            sunCount++;
        }
        else if (Random.value < .01f && entityCount < entityCountMax)
        {
            CreateEntity(cometPre, rMinCreate, player.activeCircle.radius - 5);
            entityCount++;
        }
    }
    
    private void CreateEntity(Entity e,float rMin,float rMax)
    {
        var entity = Instantiate(e,transform);
        entity.transform.position = player.transform.position +
                                   (Vector3) Random.insideUnitCircle.normalized * Random.Range(rMin, rMax);
    }
}
