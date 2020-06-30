using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoldierType
{
    FlagZombie
}

public class Soldier:BaseObject
{
    public static float Price;

    public SoldierType soldierType;

    private bool isInit = false;
    private float speed;
    
    /// <summary>
    /// 速度
    /// </summary>
    public float Speed { get { return speed; } private set { speed = value; } }
    
    /// <summary>
    /// 初始化
    /// <param name="healthy">生命值</param>
    /// </summary>
    public Soldier(float healthy, float speed, float attackDistance, SoldierType soldierType,float force)
    {
        this.soldierType = soldierType;
        this.healthy = healthy;
        this.speed = speed;
        this.attackDistance = attackDistance;
        this.force = force;
    }
}

