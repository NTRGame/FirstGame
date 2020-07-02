using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BaseObject
{
    public PlayerType playerType;
    public bool IsAlive = true;
    protected float force = 3;
    protected float frequency = 0.5f;
    protected float healthy = 10;
    protected float attackDistance;
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
    
    public void Attack(BaseObject target)
    {
        if(IsAlive && target.IsAlive)
            target.healthy -= force;
    }
}