using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Soldier
{
    public bool IsAlive = true;
    protected float force = 3;
    protected float frequency = 0.5f;
    protected float healthy = 10;
    protected float attackDistance;
    public static float Price;
    public SoldierType soldierType;
    public BulletType bulletType;
    protected float speed;
    
    /// <summary>
    /// 速度
    /// </summary>
    public float Speed { get { return speed; } private set { speed = value; } }

    /// <summary>
    /// 生命值
    /// </summary>
    public float Healthy { get { return healthy; } private set { healthy = value; } }

    /// <summary>
    /// 攻击频率
    /// </summary>
    public float Frequency { get { return frequency; } private set { frequency = value; } }

    /// <summary>
    /// 攻击距离
    /// </summary>
    public float AttackDistance { get { return attackDistance; } private set { attackDistance = value; } }

    public void Attack(Soldier target)
    {
        if (IsAlive && target.IsAlive)
            target.healthy -= force;
    }

    /// <summary>
    /// 初始化
    /// <param name="healthy">生命值</param>
    /// </summary>
    public Soldier(float healthy, float speed, float attackDistance,SoldierType soldierType, float force, float frequency,BulletType bulletType)
    {
        this.force = force;
        this.bulletType = bulletType;
        this.frequency = frequency;
        this.healthy = healthy;
        this.attackDistance = attackDistance;
        this.soldierType = soldierType;
        this.speed = speed;
    }
}

